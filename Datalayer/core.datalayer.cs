using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Core
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        public int SaveLogRequest(string Method, string Headers, string Scheme, string Host, string Path, string QueryString, string hash, string RemoteIpAddress)
        {
            try
            {
                string sql = @"";

                Debug.WriteLine(Method + " " + Headers + " " + Scheme + " " + Host + " " + Path + " " + QueryString + " " + hash + " " + RemoteIpAddress);

                return 1;//  Execute(sql, new Parameters("ID", DbType.String, id.ToLower()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public int SaveLogResponse(string Method, string Headers, string Scheme, string Host, string Path, string QueryString, string hash, string RemoteIpAddress, string response)
        {
            try
            {
                string sql = @"";

                Debug.WriteLine(Method + " " + Headers + " " + Scheme + " " + Host + " " + Path + " " + QueryString + " " + hash + " " + RemoteIpAddress + " " + response);

                return 1;//  Execute(sql, new Parameters("ID", DbType.String, id.ToLower()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}