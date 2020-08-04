using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Appointment
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
                string sql = @"SELECT * FROM appointments WHERE ID = @ID";

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
                string sql = @"SELECT * FROM appointments WHERE hash = @HASH";

                return GetDataTable(sql, new Parameters("HASH", DbType.String, hash));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Appointment appointment)
        {

            string sql = @"INSERT INTO `appointments` 
                                (`datetime`, `forced`, `allowinvoice`, 
                                `price`, `duration`, `visitmode`, `note`, 
                                `patientid`, `visittypeid`, `visitmodeid`,
                                `hash`) 
                               VALUES
	                           (@DATETIME, @FORCED, @ALLOWINVOICE, @PRICE, @DURATION, @VISITMODE, 
                                @NOTE, @PATIENTID, @VISITTYPEID, @VISITMODEID, @HASH, @OWNERID)";

            return Execute(sql, new Parameters("DATETIME", DbType.String, appointment.Datetime)
                              , new Parameters("FORCED", DbType.String, appointment.Forced)
                              , new Parameters("ALLOWINVOICE", DbType.String, appointment.AllowInvoice)
                              , new Parameters("PRICE", DbType.String, appointment.Price)
                              , new Parameters("DURATION", DbType.String, appointment.Duration)
                              , new Parameters("VISITMODE", DbType.String, appointment.VisitMode)
                              , new Parameters("NOTE", DbType.String, appointment.Note)
                              , new Parameters("PATIENTID", DbType.String, appointment.Patient.Nhc)
                              , new Parameters("VISITTYPEID", DbType.String, appointment.VisitType.Id)
                              , new Parameters("VISITMODEID", DbType.String, appointment.VisitMode.Id)
                              , new Parameters("HASH", DbType.String, appointment.Hash)
                              , new Parameters("OWNERID", DbType.String, appointment.Owner.Id)
                           );

        }

        internal int Save(Models.Appointment appointment)
        {
            string sql = @"UPDATE `appointments` 
                               SET 
                                `datetime` = @DATETIME, 
                                `forced` = @FORCED, 
                                `allowinvoice` = @ALLOWINVOICE, 
                                `price` = @PRICE, 
                                `duration` = @DURATION, 
                                `visitmode` = @VISITMODE,
                                `note` = @NOTE,
                                `patientid` = @PATIENTID,
                                `visittypeid` = @VISITTYPEID,
                                `visitmodeid` = @VISITMODEID,
                                `hash` = @HASH
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.String, appointment.Id)
                              , new Parameters("DATETIME", DbType.String, appointment.Datetime)
                              , new Parameters("FORCED", DbType.String, appointment.Forced)
                              , new Parameters("ALLOWINVOICE", DbType.String, appointment.AllowInvoice)
                              , new Parameters("PRICE", DbType.String, appointment.Price)
                              , new Parameters("DURATION", DbType.String, appointment.Duration)
                              , new Parameters("VISITMODE", DbType.String, appointment.VisitMode)
                              , new Parameters("NOTE", DbType.String, appointment.Note)
                              , new Parameters("PATIENTID", DbType.String, appointment.Patient.Nhc)
                              , new Parameters("VISITTYPEID", DbType.String, appointment.VisitType.Id)
                              , new Parameters("VISITMODEID", DbType.String, appointment.VisitMode.Id)
                              , new Parameters("HASH", DbType.String, appointment.Hash)
                          );
        }
    }
}