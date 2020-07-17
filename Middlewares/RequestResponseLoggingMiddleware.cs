using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Leaf.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IConfiguration _config;

        private string _hash;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory, 
                                                IConfiguration config)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _config = config;

        }
        public async Task Invoke(HttpContext context)
        {
            _hash = GetRequestHash();
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            /*
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}\n" +
                                   $"Schema:{context.Request.Scheme} \n" +
                                   $"Host: {context.Request.Host} \n" +
                                   $"Path: {context.Request.Path} \n" +
                                   $"QueryString: {context.Request.QueryString} \n" +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}\n\n" + _hash + "\n");
            */

            context.Request.Body.Position = 0;

            using (Leaf.Datalayers.Core.DataLayer dl = new Leaf.Datalayers.Core.DataLayer(_config))
            {
                dl.SaveLogRequest(context.Request.Method, Newtonsoft.Json.JsonConvert.SerializeObject(context.Request.Headers), context.Request.Scheme, context.Request.Host.ToString(), context.Request.Path, context.Request.QueryString.ToString(), _hash, context.Connection.RemoteIpAddress.ToString());
            }
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            /*
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}|" +
                                   $"Schema:{context.Request.Scheme} \n" +
                                   $"Host: {context.Request.Host} \n" +
                                   $"Path: {context.Request.Path} \n" +
                                   $"QueryString: {context.Request.QueryString} \n" +
                                   $"Response Body: {text}\n" + _hash + "\n");
            */

            using (Leaf.Datalayers.Core.DataLayer dl = new Leaf.Datalayers.Core.DataLayer(_config))
            {
                dl.SaveLogResponse(context.Request.Method, Newtonsoft.Json.JsonConvert.SerializeObject(context.Request.Headers), context.Request.Scheme, context.Request.Host.ToString(), context.Request.Path, context.Request.QueryString.ToString(), _hash, context.Connection.RemoteIpAddress.ToString(), text);
            }

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private string GetRequestHash()
        {
            // Get Current Datetime
            DateTime now = DateTime.Now;

            // Generate random number
            Random rand = new Random();
            string random = rand.Next().ToString();

            // We use now datetime + random number to generate a MD5 hash
            string input = now.ToString("dd/MM/yyyy HH:mm:ss-") + random;

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}