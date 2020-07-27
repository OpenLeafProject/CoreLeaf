using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.UserProfile
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
                string sql = @"SELECT * FROM user_profiles WHERE ID = @ID";

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
                string sql = @"SELECT * FROM user_profiles WHERE CODE = @CODE";

                return GetDataTable(sql, new Parameters("CODE", DbType.String, code));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.UserProfile center)
        {

            string sql = @"INSERT INTO `user_profiles` 
                                (`code`, `description`, `creationdate`) 
                               VALUES
	                           (@CODE, @DESCRIPTION, NOW())";

            return Execute(sql, new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("DESCRIPTION", DbType.String, center.Description)
                                   );

        }

        internal int Save(Models.UserProfile center)
        {
            string sql = @"UPDATE `user_profiles` 
                               SET 
                                `code` = @CODE, 
                                `description` = @DESCRIPTION, 
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, center.Id)
                                   , new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("DESCRIPTION", DbType.String, center.Description)
                                   );
        }
    }
}