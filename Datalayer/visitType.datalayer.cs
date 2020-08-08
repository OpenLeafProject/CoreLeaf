using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.VisitType
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
                string sql = @"SELECT * FROM visit_types WHERE ID = @ID";

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
                string sql = @"SELECT * FROM visit_types WHERE CODE = @CODE";

                return GetDataTable(sql, new Parameters("CODE", DbType.String, code));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.VisitType center)
        {

            string sql = @"INSERT INTO `visit_types` 
                                (`code`, `description`, `creationdate`, `color`) 
                               VALUES
	                           (@CODE, @DESCRIPTION, NOW(), @COLOR)";

            return Execute(sql, new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("DESCRIPTION", DbType.String, center.Description)
                                   , new Parameters("COLOR", DbType.String, center.Description)
                                   );

        }

        internal ActionResult<object> GetAll(IConfiguration _config)
        {
            string sql = @"SELECT * FROM visit_types";

            DataTable dt = GetDataTable(sql);
            List<Models.VisitType> list = new List<Models.VisitType>();

            foreach (DataRow row in dt.Rows)
            {

                Models.VisitType tmp = new Models.VisitType(Int32.Parse(row["id"].ToString()), _config);

                list.Add(tmp);
            }

            return list;
        }

        internal int Save(Models.VisitType center)
        {
            string sql = @"UPDATE `visit_types` 
                               SET 
                                `code` = @CODE, 
                                `description` = @DESCRIPTION, 
                                `color` = @COLOR
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, center.Id)
                                   , new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("DESCRIPTION", DbType.String, center.Description)
                                   , new Parameters("COLOR", DbType.String, center.Description)

                                   );
        }
    }
}