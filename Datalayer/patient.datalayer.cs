using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Patient
{
    public class DataLayer : Leaf.Data.DataLayerBaseMySQL
    {

        public DataLayer(IConfiguration _config)
            : base(new Data.ConnectionDB(_config).getConnectString())
        {

        }

        public DataTable Test(string id)
        {
            try
            {
                string sql = @"SELECT * FROM users WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.String, id.ToLower()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}