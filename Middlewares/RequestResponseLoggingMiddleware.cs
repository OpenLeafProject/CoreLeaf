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
            _hash = Leaf.Tools.MD5Tools.GetRequestHash();
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

            if (_config.GetValue<Boolean>("Run:EnableDBLog"))
            {
                string logheaders = String.Empty;
                if(_config.GetValue<Boolean>("Run:HardLog"))
                {
                    logheaders = Newtonsoft.Json.JsonConvert.SerializeObject(context.Request.Headers);
                }
                using (Leaf.Datalayers.Core.DataLayer dl = new Leaf.Datalayers.Core.DataLayer(_config))
                {
                    dl.SaveLogRequest(context.Request.Method, logheaders, context.Request.Scheme, context.Request.Host.ToString(), context.Request.Path, context.Request.QueryString.ToString(), _hash, context.Connection.RemoteIpAddress.ToString());
                }
            }
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

            if (_config.GetValue<Boolean>("Run:EnableDBLog") && _config.GetValue<Boolean>("Run:HardLog"))
            {
                using (Leaf.Datalayers.Core.DataLayer dl = new Leaf.Datalayers.Core.DataLayer(_config))
                {
                    dl.SaveLogResponse(context.Request.Method, Newtonsoft.Json.JsonConvert.SerializeObject(context.Request.Headers), context.Request.Scheme, context.Request.Host.ToString(), context.Request.Path, context.Request.QueryString.ToString(), _hash, context.Connection.RemoteIpAddress.ToString(), text);
                }
            }


            await responseBody.CopyToAsync(originalBodyStream);
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