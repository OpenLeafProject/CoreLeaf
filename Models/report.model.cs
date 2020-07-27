using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Report
    {
        private int id;
        private string content;
        private string hash;
        private DateTime creationDate;
        private DateTime signDate;
        private Patient patient;
        private Appointment appointment;

        private readonly IConfiguration _config;


        public int Id { get => id; }
        public string Content { get => content; set => content = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime SignDate { get => signDate; set => signDate = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Appointment Appointment { get => appointment; set => appointment = value; }
        public string Hash { get => hash; set => hash = value; }

        public Report(IConfiguration config)
        {
            _config = config;
        }

        public Report(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public Report(string hash, IConfiguration config)
        {
            _config = config;
            LoadByHash(hash);
        }

        public Report(Dictionary<string, string> values, IConfiguration config)
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
            Content = values["content"];
            CreationDate = DateTime.Parse(values["creationDate"]);
            SignDate = DateTime.Parse(values["signDate"]);
            Patient = new Patient(Int32.Parse(values["patientid"]), _config);
            Appointment = new Appointment(Int32.Parse(values["appointmentid"]), _config);
        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.Report.DataLayer dl = new Leaf.Datalayers.Report.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Content = dt.Rows[0]["content"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationDate"].ToString());
                    SignDate = DateTime.Parse(dt.Rows[0]["signDate"].ToString());
                    Patient = new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config);
                    Appointment = new Appointment(Int32.Parse(dt.Rows[0]["appointmentid"].ToString()), _config);
                }
                else
                {
                    throw new System.NullReferenceException("Report not found");
                }
            }
        }

        private void LoadByHash(string hash)
        {
            using (Leaf.Datalayers.Report.DataLayer dl = new Leaf.Datalayers.Report.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadByHash(hash);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Content = dt.Rows[0]["content"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationDate"].ToString());
                    SignDate = DateTime.Parse(dt.Rows[0]["signDate"].ToString());
                    Patient = new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config);
                    Appointment = new Appointment(Int32.Parse(dt.Rows[0]["appointmentid"].ToString()), _config);

                }
                else
                {
                    throw new System.NullReferenceException("Report not found");
                }
            }
        }

        public Report Create()
        {
            using (Leaf.Datalayers.Report.DataLayer dl = new Leaf.Datalayers.Report.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create report with setted Id");
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

        public Report Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save report without setted Id");
            }

            using (Leaf.Datalayers.Report.DataLayer dl = new Leaf.Datalayers.Report.DataLayer(_config))
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
