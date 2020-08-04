using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Report
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
                string sql = @"SELECT * FROM reports WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal DataTable loadByHash(string hash)
        {
            try
            {
                string sql = @"SELECT * FROM reports WHERE HASH = @HASH";

                return GetDataTable(sql, new Parameters("HASH", DbType.String, hash));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Report report)
        {

            string sql = @"INSERT INTO `reports` 
                                (`content`, `creationdate`, 
                                `patientid`, `appointmentid`, `hash`, `ownerid`) 
                               VALUES
	                           (@CONTENT,  NOW(), @PATIENTID, @APPOINTMENTID, @HASH, @OWNERID)";

            return Execute(sql, new Parameters("CONTENT", DbType.String, report.Content)
                                   , new Parameters("PATIENTID", DbType.Int32, report.Patient.Nhc)
                                   , new Parameters("APPOINTMENTID", DbType.String, report.Appointment.Id)
                                   , new Parameters("HASH", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(report.Content + report.Patient.Nhc))
                                   , new Parameters("OWNERID", DbType.String, report.Owner.Id)
                                   );

        }

        internal int Save(Models.Report report)
        {
            string sql = @"UPDATE `reports` 
                               SET 
                                `content` = @CONTENT, 
                                `patientid` = @PATIENTID, 
                                `appointmentid` = @APPOINTMENTID, 
                                `hash` = @HASH
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, report.Id)
                                   , new Parameters("CONTENT", DbType.String, report.Content)
                                   , new Parameters("PATIENTID", DbType.Int32, report.Patient.Nhc)
                                   , new Parameters("APPOINTMENTID", DbType.String, report.Appointment.Id)
                                   , new Parameters("HASH", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(report.Content + report.Patient.Nhc))
                                   );
        }
        internal int Sign(Models.Report report)
        {
            report.SignDate = DateTime.Now;

            string sql = @"UPDATE `reports` 
                               SET 
                                `signdate` = NOW(), 
                                `hash` = @HASH
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.Int32, report.Id)
                                   , new Parameters("SIGNDATE", DbType.DateTime, report.SignDate)
                                   , new Parameters("HASH", DbType.String, Leaf.Tools.MD5Tools.GetMd5Hash(report.Content + report.Patient.Nhc + report.SignDate.ToString("dd/MM/yyyy HH:mm:ss")))
                                   );
        }

    }
}