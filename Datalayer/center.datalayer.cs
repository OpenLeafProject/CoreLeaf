using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Center
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        public DataTable loadCenterById(int id)
        {
            try
            {
                string sql = @"SELECT * FROM centers WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public DataTable loadCenterByCode(string code)
        {
            try
            {
                string sql = @"SELECT * FROM center WHERE CODE = @CODE";

                return GetDataTable(sql, new Parameters("CODE", DbType.String, code));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}