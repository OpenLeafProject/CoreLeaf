using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Note
    {
        private int id;
        private string content;
        private string hash;
        private DateTime creationDate;
        private Patient patient;
        private User owner;

        private readonly IConfiguration _config;


        public int Id { get => id; }
        public string Content { get => content; set => content = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public string Hash { get => hash; set => hash = value; }
        public User Owner { get => owner; set => owner = value; }

        public Note(IConfiguration config)
        {
            _config = config;
        }

        public Note(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public Note(Dictionary<string, string> values, IConfiguration config)
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
            Hash = values["hash"];
            CreationDate = DateTime.Parse(values["creationDate"]);
            Patient = new Patient(Int32.Parse(values["patientid"]), _config);
            Owner = new User(Int32.Parse(values["ownerid"]), _config);

        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.Note.DataLayer dl = new Leaf.Datalayers.Note.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Content = dt.Rows[0]["content"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationDate"].ToString());
                    Patient = new Patient(Int32.Parse(dt.Rows[0]["patientid"].ToString()), _config);
                    Owner = new User(Int32.Parse(dt.Rows[0]["ownerid"].ToString()), _config);
                }
                else
                {
                    throw new System.NullReferenceException("Report not found");
                }
            }
        }

        public Note Create()
        {
            using (Leaf.Datalayers.Note.DataLayer dl = new Leaf.Datalayers.Note.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create report with setted Id");
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

        public Note Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save report without setted Id");
            }

            using (Leaf.Datalayers.Note.DataLayer dl = new Leaf.Datalayers.Note.DataLayer(_config))
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
