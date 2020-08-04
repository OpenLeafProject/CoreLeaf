using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Diagnostics;

namespace Leaf.Datalayers.Invoice
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
                string sql = @"SELECT * FROM invoices WHERE ID = @ID";

                return GetDataTable(sql, new Parameters("ID", DbType.Int32, id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        internal int Create(Models.Invoice invoice)
        {

            string sql = @"INSERT INTO `invoices` 
                                (`ammount`, `iva`, `invoicedate`, 
                                `paymentdate`, `patientid`, `appointmentid`, `centerid`, 
                                `paymentmethodid`) 
                               VALUES
	                           (@AMMOUNT, @IVA, @INVOICEDATE, @PAYMENTDATE, @PATIENTID, @APPOINTMENTID, 
                                @CENTERID, @PAYMENTMETHODID, @OWNERID); LAST_INSERT_ID();";

            return Int32.Parse(GetScalar(sql, new Parameters("AMMOUNT", DbType.String, invoice.Ammount)
                              , new Parameters("IVA", DbType.String, invoice.Iva)
                              , new Parameters("INVOICEDATE", DbType.String, invoice.InvoiceDate)
                              , new Parameters("PAYMENTDATE", DbType.String, invoice.PaymentDate)
                              , new Parameters("PATIENTID", DbType.String, invoice.Patient.Nhc)
                              , new Parameters("APPOINTMENTID", DbType.String, invoice.Appointment.Id)
                              , new Parameters("CENTERID", DbType.String, invoice.Center.Id)
                              , new Parameters("PAYMENTMETHODID", DbType.String, invoice.PaymentMethod.Id)
                              , new Parameters("OWNERID", DbType.String, invoice.Owner.Id)
                           ).ToString());

        }

        internal int Save(Models.Invoice invoice)
        {
            string sql = @"UPDATE `invoices` 
                               SET 
                                `ammount` = @AMMOUNT, 
                                `iva` = @IVA, 
                                `invoicedate` = @INVOICEDATE, 
                                `paymentdate` = @PAYMENTDATE, 
                                `patientid` = @PATIENTID, 
                                `appointmentid` = @APPOINTMENTID,
                                `centerid` = @CENTERID,
                                `paymentmethodid` = @PAYMENTMETHODID
                               WHERE `id` = @ID";

            return Execute(sql, new Parameters("ID", DbType.String, invoice.Id)
                              , new Parameters("AMMOUNT", DbType.String, invoice.Ammount)
                              , new Parameters("IVA", DbType.String, invoice.Iva)
                              , new Parameters("INVOICEDATE", DbType.String, invoice.InvoiceDate)
                              , new Parameters("PAYMENTDATE", DbType.String, invoice.PaymentDate)
                              , new Parameters("PATIENTID", DbType.String, invoice.Patient.Nhc)
                              , new Parameters("APPOINTMENTID", DbType.String, invoice.Appointment.Id)
                              , new Parameters("CENTERID", DbType.String, invoice.Center.Id)
                              , new Parameters("PAYMENTMETHODID", DbType.String, invoice.PaymentMethod.Id)
                           );
        }
    }
}