using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.ApiLog
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        internal DataTable loadById(string id)
        {
            try
            {
                string sql = @"SELECT * FROM api_log WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.ApiLog apilog)
        {

            string sql = @"INSERT INTO `api_log` 
                                (`id`, `method`, `scheme`, `host`, `path`,
                                `querystring`, `remoteipadress`, `response`, 
                                `headers`, `insertdate`,  `updatedate`) 
                               VALUES
	                           (@ID, @METHOD, @SCHEME, @HOST, @PATH, @QUERYSTRING, @REMOTEIPADRESS, @RESPONSE,
                                @HEADERS, NOW(), NOW())";

            return Execute(sql, new Parameters("ID", DbType.String, apilog.Id)
                                   , new Parameters("METHOD", DbType.String, apilog.Method)
                                   , new Parameters("SCHEME", DbType.String, apilog.Scheme)
                                   , new Parameters("HOST", DbType.String, apilog.Host)
                                   , new Parameters("PATH", DbType.String, apilog.Path)
                                   , new Parameters("QUERYSTRING", DbType.String, apilog.QueryString)
                                   , new Parameters("REMOTEIPADRESS", DbType.String, apilog.RemoteIPAdress)
                                   , new Parameters("RESPONSE", DbType.String, apilog.Response)
                                   , new Parameters("HEADERS", DbType.String, apilog.Headers.Replace("'",""))
                                   );

        }

        internal int Save(Models.ApiLog apilog)
        {
            string sql = @"UPDATE `api_log` 
                               SET 
                                `response` = @RESPONSE, 
                                `updatedate` = NOW()
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.String, apilog.Id)
                                   , new Parameters("RESPONSE", DbType.String, apilog.Response)
                                   );

        }
    }
}