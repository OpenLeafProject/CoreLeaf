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

        internal DataTable loadById(int id)
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

        internal DataTable loadByCode(string code)
        {
            try
            {
                string sql = @"SELECT * FROM centers WHERE CODE = @CODE";

                return GetDataTable(sql, new Parameters("CODE", DbType.String, code));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Center center)
        {

            string sql = @"INSERT INTO `centers` 
                                (`code`, `name`, `nif`, 
                                `address`, `city`, `pc`, `creationdate`) 
                               VALUES
	                           (@CODE, @NAME, @NIF, @ADDRESS, @CITY, @PC, NOW())";

            return Execute(sql, new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("NAME", DbType.String, center.Name)
                                   , new Parameters("NIF", DbType.String, center.Nif)
                                   , new Parameters("ADDRESS", DbType.String, center.Address)
                                   , new Parameters("CITY", DbType.String, center.City)
                                   , new Parameters("PC", DbType.String, center.Pc)
                                   );

        }

        internal int Save(Models.Center center)
        {
            string sql = @"UPDATE `centers` 
                               SET 
                                `code` = @CODE, 
                                `name` = @NAME, 
                                `nif` = @NIF, 
                                `address` = @ADDRESS, 
                                `city` = @CITY, 
                                `pc` = @PC 
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, center.Id)
                                   , new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("NAME", DbType.String, center.Name)
                                   , new Parameters("NIF", DbType.String, center.Nif)
                                   , new Parameters("ADDRESS", DbType.String, center.Address)
                                   , new Parameters("CITY", DbType.String, center.City)
                                   , new Parameters("PC", DbType.String, center.Pc)
                                   );
        }
    }
}