using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Appointment
    {
        private int id;
        private DateTime datetime;
        private int forced;
        private int allowInvoice;
        private Double price;
        private int duration;
        private int status;
        private string note;
        private string hash;
        private Patient patient;
        private VisitType visitType;
        private VisitMode visitMode;
        private User owner;

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public int Forced { get => forced; set => forced = value; }
        public int AllowInvoice { get => allowInvoice; set => allowInvoice = value; }
        public Double Price { get => price; set => price = value; }
        public int Duration { get => duration; set => duration = value; }
        public VisitMode VisitMode { get => visitMode; set => visitMode = value; }
        public string Note { get => note; set => note = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public VisitType VisitType { get => visitType; set => visitType = value; }
        public string Hash { get => hash; set => hash = value; }
        public User Owner { get => owner; set => owner = value; }
        public int Status { get => status; set => status = value; }

        public Appointment(IConfiguration config)
        {
            _config = config;
        }

        public Appointment(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public Appointment(string hash, IConfiguration config)
        {
            _config = config;
            LoadByHash(hash);
        }

        public Appointment(Dictionary<string, string> values, IConfiguration config)
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
            Datetime = DateTime.Parse(values["datetime"]);
            Forced = Int32.Parse(values["forced"]);
            AllowInvoice = Int32.Parse(values["allowinvoice"]);
            Status = Int32.Parse(values["status"]);
            Duration = Int32.Parse(values["duration"]);
            Note = values["note"];

            try
            {
                Price = Double.Parse(values["price"]);
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Price = 0;
            }


            try
            {
                Patient = values["patientid"] != "" ? new Patient(Int32.Parse(values["patientid"]), _config) : null;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Patient = null;
            }

            try
            {
                VisitType = values["visittypeid"] != "" ? new VisitType(Int32.Parse(values["visittypeid"]), _config) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                VisitType = null;
            }
            try {
                VisitMode = values["visitmodeid"] != "" ? new VisitMode(Int32.Parse(values["visitmodeid"]), _config) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                VisitMode = null;
            }
            try { 
                Owner = new User(Int32.Parse(values["ownerid"].ToString()), _config);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Owner = null;
            }

            Hash = Tools.MD5Tools.GetMd5Hash(Datetime.ToString() + Duration);
        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.Appointment.DataLayer dl = new Leaf.Datalayers.Appointment.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Datetime = DateTime.Parse(dt.Rows[0]["datetime"].ToString());
                    Forced = Int32.Parse(dt.Rows[0]["forced"].ToString());
                    AllowInvoice = Int32.Parse(dt.Rows[0]["allowinvoice"].ToString());
                    Price = Double.Parse(dt.Rows[0]["price"].ToString());
                    Duration = Int32.Parse(dt.Rows[0]["duration"].ToString());
                    Status = Int32.Parse(dt.Rows[0]["status"].ToString());
                    Note = dt.Rows[0]["note"].ToString();
                    Patient = dt.Rows[0]["patientid"].ToString() != "" ? new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config) : null;
                    VisitType = new VisitType(Int32.Parse(dt.Rows[0]["visittypeid"].ToString()), _config);
                    VisitMode = new VisitMode(Int32.Parse(dt.Rows[0]["visitmodeid"].ToString()), _config);
                    Owner = new User(Int32.Parse(dt.Rows[0]["ownerid"].ToString()), _config);


                }
                else
                {
                    throw new System.NullReferenceException("Appointment not found");
                }
            }
        }

        private void LoadByHash(string hash)
        {
            using (Leaf.Datalayers.Appointment.DataLayer dl = new Leaf.Datalayers.Appointment.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadByHash(hash);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Datetime = DateTime.Parse(dt.Rows[0]["datetime"].ToString());
                    Forced = Int32.Parse(dt.Rows[0]["forced"].ToString());
                    AllowInvoice = Int32.Parse(dt.Rows[0]["allowinvoice"].ToString());
                    Price = Double.Parse(dt.Rows[0]["price"].ToString());
                    Duration = Int32.Parse(dt.Rows[0]["duration"].ToString());
                    Status = Int32.Parse(dt.Rows[0]["status"].ToString());
                    Note = dt.Rows[0]["note"].ToString();
                    Patient = dt.Rows[0]["patientid"].ToString() != "" ? new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config) : null;
                    VisitType = dt.Rows[0]["patientid"].ToString() != "" ? new VisitType(Int32.Parse(dt.Rows[0]["visittypeid"].ToString()), _config) : null;
                    VisitMode = dt.Rows[0]["patientid"].ToString() != "" ? new VisitMode(Int32.Parse(dt.Rows[0]["visitmodeid"].ToString()), _config) : null;
                    Owner = dt.Rows[0]["patientid"].ToString() != "" ? new User(Int32.Parse(dt.Rows[0]["ownerid"].ToString()), _config) : null;

                }
                else
                {
                    throw new System.NullReferenceException("Appointment not found");
                }
            }
        }

        public Appointment Create()
        {
            using (Leaf.Datalayers.Appointment.DataLayer dl = new Leaf.Datalayers.Appointment.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create appointment with setted Id");
                }

                if (dl.Create(this) == 1)
                {
                    LoadByHash(this.Hash);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public Appointment Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save appointment without setted Id");
            }

            using (Leaf.Datalayers.Appointment.DataLayer dl = new Leaf.Datalayers.Appointment.DataLayer(_config))
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
