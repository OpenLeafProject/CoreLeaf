using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
                                `price`, `duration`, `note`, 
                                `patientid`, `visittypeid`, `visitmodeid`,
                                `hash`, `ownerid`, `status`) 
                               VALUES
	                           (@DATETIME, @FORCED, @ALLOWINVOICE, @PRICE, @DURATION, 
                                @NOTE, @PATIENTID, @VISITTYPEID, @VISITMODEID, @HASH, @OWNERID, 0)";

            return Execute(sql, new Parameters("DATETIME", DbType.String, appointment.Datetime)
                              , new Parameters("FORCED", DbType.String, appointment.Forced)
                              , new Parameters("ALLOWINVOICE", DbType.String, appointment.AllowInvoice)
                              , new Parameters("PRICE", DbType.String, appointment.Price)
                              , new Parameters("DURATION", DbType.String, appointment.Duration)
                              , new Parameters("VISITMODE", DbType.String, appointment.VisitMode)
                              , new Parameters("NOTE", DbType.String, appointment.Note)
                              , new Parameters("PATIENTID", DbType.String, (appointment.Patient != null ? appointment.Patient.Nhc.ToString() : null))
                              , new Parameters("VISITTYPEID", DbType.String, (appointment.VisitType != null ? appointment.VisitType.Id.ToString() : null))
                              , new Parameters("VISITMODEID", DbType.String, (appointment.VisitMode != null ? appointment.VisitMode.Id.ToString() : null))
                              , new Parameters("OWNERID", DbType.String, (appointment.Owner != null ? appointment.Owner.Id.ToString() : null))
                              , new Parameters("HASH", DbType.String, appointment.Hash)
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
                                `note` = @NOTE,
                                `patientid` = @PATIENTID,
                                `visittypeid` = @VISITTYPEID,
                                `visitmodeid` = @VISITMODEID,
                                `hash` = @HASH,
                                `status` = @STATUS,
                                `ownerid` = @OWNERID

                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.String, appointment.Id)
                              , new Parameters("DATETIME", DbType.String, appointment.Datetime)
                              , new Parameters("FORCED", DbType.String, appointment.Forced)
                              , new Parameters("ALLOWINVOICE", DbType.String, appointment.AllowInvoice)
                              , new Parameters("PRICE", DbType.String, appointment.Price)
                              , new Parameters("DURATION", DbType.String, appointment.Duration)
                              , new Parameters("NOTE", DbType.String, appointment.Note)
                              , new Parameters("PATIENTID", DbType.String, (appointment.Patient != null ? appointment.Patient.Nhc.ToString() : null))
                              , new Parameters("VISITTYPEID", DbType.String, (appointment.VisitType != null ? appointment.VisitType.Id.ToString() : null))
                              , new Parameters("VISITMODEID", DbType.String, (appointment.VisitMode != null ? appointment.VisitMode.Id.ToString() : null))
                              , new Parameters("OWNERID", DbType.String, (appointment.Owner != null ? appointment.Owner.Id.ToString() : null))
                              , new Parameters("HASH", DbType.String, appointment.Hash)
                              , new Parameters("STATUS", DbType.Int32, appointment.Status)
                          );
        }

        internal ActionResult<object> GetFullCalendar(Models.User user, IConfiguration _config)
        {
            try
            {
                string sql = @"SELECT appointments.*, visit_types.color
                               FROM appointments, visit_types 
                               WHERE appointments.visittypeid = visit_types.id AND
                                     OWNERID = @OWNERID";

                DataTable dt = GetDataTable(sql, new Parameters("OWNERID", DbType.Int32, user.Id));
                List<Models.CalendarAppointment> appointments = new List<Models.CalendarAppointment>();

                foreach (DataRow row in dt.Rows)
                {

                    Models.CalendarAppointment tmpAppointment = new Models.CalendarAppointment();
                    tmpAppointment.Id = Int32.Parse(row["id"].ToString());
                    tmpAppointment.Start = DateTime.Parse(row["datetime"].ToString());
                    tmpAppointment.End = DateTime.Parse(row["datetime"].ToString()).AddMinutes(Int32.Parse(row["duration"].ToString()));
                    tmpAppointment.Color = row["color"].ToString();

                    try
                    {
                        Models.Patient tmpPatient = new Models.Patient(Int32.Parse(row["patientid"].ToString()), _config);
                        tmpAppointment.Title = tmpPatient.Name + ' ' + tmpPatient.Surname;
                        tmpAppointment.Title += (row["note"].ToString() != "" && row["note"].ToString() != null) ? (" - " + row["note"].ToString()) : "";
                    } catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        tmpAppointment.Title = row["note"].ToString();
                    }

                    appointments.Add(tmpAppointment);
                }

                return appointments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal ActionResult<object> GetTodaysList(Models.User user, IConfiguration _config)
        {
            try
            {
                string sql = @"SELECT * FROM appointments WHERE OWNERID = @OWNERID AND DATE(DATETIME) = CURDATE()";

                DataTable dt = GetDataTable(sql, new Parameters("OWNERID", DbType.Int32, user.Id));
                List<Models.Appointment> appointments = new List<Models.Appointment>();

                foreach (DataRow row in dt.Rows)
                {
                    Models.Appointment tmpAppointment = new Models.Appointment(
                        new Dictionary<string, string> {
                        { "id",             row["id"].ToString() },
                        { "datetime",       row["datetime"].ToString() },
                        { "forced",         row["forced"].ToString() },
                        { "allowinvoice",   row["allowinvoice"].ToString() },
                        { "price",          row["price"].ToString() },
                        { "duration",       row["duration"].ToString() },
                        { "note",           row["note"].ToString() },
                        { "status",         row["status"].ToString() },
                        { "patientid",      row["patientid"].ToString() },
                        { "visittypeid",    row["visittypeid"].ToString() },
                        { "visitmodeid",    row["visitmodeid"].ToString() },
                        { "ownerid",        row["ownerid"].ToString() }
                    }, _config);

                    appointments.Add(tmpAppointment);
                }

                return appointments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}