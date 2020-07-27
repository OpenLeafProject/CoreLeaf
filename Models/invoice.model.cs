using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Invoice
    {
        private int id;
        private Double ammount;
        private Double iva;
        private DateTime invoiceDate;
        private DateTime paymentDate;
        private Patient patient;
        private Appointment appointment;
        private Center center;
        private PaymentMethod paymentMethod;

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public double Ammount { get => ammount; set => ammount = value; }
        public double Iva { get => iva; set => iva = value; }
        public DateTime InvoiceDate { get => invoiceDate; set => invoiceDate = value; }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Appointment Appointment { get => appointment; set => appointment = value; }
        public Center Center { get => center; set => center = value; }
        public PaymentMethod PaymentMethod { get => paymentMethod; set => paymentMethod = value; }


        public Invoice(IConfiguration config)
        {
            _config = config;
        }

        public Invoice(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public Invoice(Dictionary<string, string> values, IConfiguration config)
        {
            _config = config;

            try
            {
                this.id = Int32.Parse(values["id"]);
                LoadById(this.id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                this.id = -1;
            }

            this.id = Int32.Parse(values["id"]);
            Ammount = Double.Parse(values["ammount"]);
            Iva = Double.Parse(values["iva"]);
            InvoiceDate = DateTime.Parse(values["invoicedate"]);
            PaymentDate = DateTime.Parse(values["paymentdate"]);
            Patient = new Patient(Int32.Parse(values["patientid"]), _config);
            Appointment = new Appointment(Int32.Parse(values["appointmentid"]), _config);
            Center = new Center(Int32.Parse(values["centerid"]), _config);
            PaymentMethod = new PaymentMethod(Int32.Parse(values["paymentmethodid"]), _config);

        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.Invoice.DataLayer dl = new Leaf.Datalayers.Invoice.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Ammount = Double.Parse(dt.Rows[0]["ammount"].ToString());
                    Iva = Double.Parse(dt.Rows[0]["iva"].ToString());
                    InvoiceDate = DateTime.Parse(dt.Rows[0]["invoicedate"].ToString());
                    PaymentDate = DateTime.Parse(dt.Rows[0]["paymentdate"].ToString());
                    Patient = new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config);
                    Appointment = new Appointment(Int32.Parse(dt.Rows[0]["appointmentid"].ToString()), _config);
                    Center = new Center(Int32.Parse(dt.Rows[0]["centerid"].ToString()), _config);
                    PaymentMethod = new PaymentMethod(Int32.Parse(dt.Rows[0]["paymentmethodid"].ToString()), _config);

                }
                else
                {
                    throw new System.NullReferenceException("Invoice not found");
                }
            }
        }

        public Invoice Create()
        {
            using (Leaf.Datalayers.Invoice.DataLayer dl = new Leaf.Datalayers.Invoice.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create invoice with setted Id");
                }

                int returnedid = dl.Create(this);

                if (returnedid > 0)
                {
                    this.id = returnedid;
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public Invoice Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save invoice without setted Id");
            }

            using (Leaf.Datalayers.Invoice.DataLayer dl = new Leaf.Datalayers.Invoice.DataLayer(_config))
            {
                if (dl.Save(this) == 1)
                {
                    LoadById(this.Id);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
