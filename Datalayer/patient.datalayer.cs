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

        internal DataTable Test(string id)
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

        internal DataTable GetByNHC(int nhc)
        {
            try
            {
                string sql = @"SELECT * FROM patients WHERE NHC = @NHC";

                return GetDataTable(sql, new Parameters("NHC", DbType.Int32, nhc));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal DataTable GetByDNI(string dni)
        {
            try
            {
                string sql = @"SELECT * FROM patients WHERE DNI = @DNI";

                return GetDataTable(sql, new Parameters("DNI", DbType.String, dni));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Patient patient)
        {

                string sql = @"INSERT INTO `patients` 
                                (`name`, `surname`, `lastname`, 
                                `dni`, `phone`, `phonealt`, `address`, 
                                `city`, `pc`, `sex`, `note`, `lastaccess`, 
                                `email`, `centerid`, `borndate`) 
                               VALUES
	                           (@NAME, @SURNAME, @LASTNAME, @DNI, @PHONE, @PHONEALT, @ADDRESSS, @CITY, 
                                @PC, @SEX, @NOTE, @LASTACCESS, @EMAIL, @CENTERID, @BORNDATE)";

                return Execute(sql, new Parameters("NAME", DbType.String, patient.Name)
                                       , new Parameters("SURNAME", DbType.String, patient.Surname)
                                       , new Parameters("LASTNAME", DbType.String, patient.Lastname)
                                       , new Parameters("DNI", DbType.String, patient.Dni)
                                       , new Parameters("PHONE", DbType.String, patient.Phone)
                                       , new Parameters("PHONEALT", DbType.String, patient.PhoneAlt)
                                       , new Parameters("ADDRESSS", DbType.String, patient.Address)
                                       , new Parameters("CITY", DbType.String, patient.City)
                                       , new Parameters("PC", DbType.Int32, patient.Pc)
                                       , new Parameters("SEX", DbType.Int32, patient.Sex)
                                       , new Parameters("NOTE", DbType.String, patient.Note)
                                       , new Parameters("LASTACCESS", DbType.Int32, patient.LastAccess)
                                       , new Parameters("EMAIL", DbType.Int32, patient.Email)
                                       , new Parameters("CENTERID", DbType.Int32, patient.Center.Id)
                                       , new Parameters("BORNDATE", DbType.Int32, patient.BornDate)
                                       );

        }

        internal int Save(Models.Patient patient)
        {
                string sql = @"UPDATE `patients` 
                               SET 
                                `name` = @NAME, 
                                `surname` = @SURNAME, 
                                `lastname` = @LASTNAME, 
                                `dni` = @DNI, 
                                `phone` = @PHONE, 
                                `phonealt` = @PHONEALT, 
                                `address` = @ADDRESSS, 
                                `city` = @CITY, 
                                `pc` = @PC, 
                                `sex` = @SEX, 
                                `note` = @NOTE, 
                                `lastaccess` = @LASTACCESS, 
                                `email` = @EMAIL, 
                                `centerid` = @CENTERID, 
                                `borndate` = @BORNDATE
                               WHERE `nhc` = @NHC";

                return Execute(sql, new Parameters("NHC", DbType.Int32, patient.Name)
                                       , new Parameters("NAME", DbType.String, patient.Name)
                                       , new Parameters("SURNAME", DbType.String, patient.Surname)
                                       , new Parameters("LASTNAME", DbType.String, patient.Lastname)
                                       , new Parameters("DNI", DbType.String, patient.Dni)
                                       , new Parameters("PHONE", DbType.String, patient.Phone)
                                       , new Parameters("PHONEALT", DbType.String, patient.PhoneAlt)
                                       , new Parameters("ADDRESSS", DbType.String, patient.Address)
                                       , new Parameters("CITY", DbType.String, patient.City)
                                       , new Parameters("PC", DbType.Int32, patient.Pc)
                                       , new Parameters("SEX", DbType.Int32, patient.Sex)
                                       , new Parameters("NOTE", DbType.String, patient.Note)
                                       , new Parameters("LASTACCESS", DbType.Int32, patient.LastAccess)
                                       , new Parameters("EMAIL", DbType.Int32, patient.Email)
                                       , new Parameters("CENTERID", DbType.Int32, patient.Center.Id)
                                       , new Parameters("BORNDATE", DbType.Int32, patient.BornDate)
                                       );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}