using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.VisitMode
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
                string sql = @"SELECT * FROM visit_modes WHERE ID = @ID";

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
                string sql = @"SELECT * FROM visit_modes WHERE CODE = @CODE";

                return GetDataTable(sql, new Parameters("CODE", DbType.String, code));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.VisitMode center)
        {

            string sql = @"INSERT INTO `visit_modes` 
                                (`code`, `description`, `creationdate`) 
                               VALUES
	                           (@CODE, @DESCRIPTION, NOW())";

            return Execute(sql, new Parameters("CODE", DbType.String, center.Code)
                                   , new Parameters("DESCRIPTION", DbType.String, center.Description)
                                   );

        }

        internal ActionResult<object> GetAll(IConfiguration _config)
        {
            string sql = @"SELECT * FROM visit_modes";

            DataTable dt = GetDataTable(sql);
            List<Models.VisitMode> list = new List<Models.VisitMode>();

            foreach (DataRow row in dt.Rows)
            {

                Models.VisitMode tmp = new Models.VisitMode(Int32.Parse(row["id"].ToString()), _config);

                list.Add(tmp);
            }

            return list;
        }

        internal int Save(Models.VisitMode center)
        {
            string sql = @"UPDATE `visit_modes` 
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