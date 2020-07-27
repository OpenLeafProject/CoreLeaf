using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class VisitType
    {
        private int id;
        private string description;
        private string code;
        private DateTime creationDate;

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public string Description { get => description; set => description = value; }
        public string Code { get => code; set => code = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public VisitType(IConfiguration config)
        {
            _config = config;
        }

        public VisitType(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public VisitType(string code, IConfiguration config)
        {
            _config = config;
            LoadByCode(code);
        }

        public VisitType(Dictionary<string, string> values, IConfiguration config)
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

            Code = values["code"];
            Description = values["description"];
            CreationDate = DateTime.Parse(values["creationDate"]);

        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.VisitType.DataLayer dl = new Leaf.Datalayers.VisitType.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Description = dt.Rows[0]["description"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationdate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("VisitType not found");
                }
            }
        }

        private void LoadByCode(string code)
        {
            using (Leaf.Datalayers.VisitType.DataLayer dl = new Leaf.Datalayers.VisitType.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadByCode(code);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Description = dt.Rows[0]["description"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationdate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("VisitType not found");
                }
            }
        }

        public VisitType Create()
        {
            using (Leaf.Datalayers.VisitType.DataLayer dl = new Leaf.Datalayers.VisitType.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create visitType with setted Id");
                }

                if (dl.Create(this) == 1)
                {
                    LoadByCode(this.Code);
                    return this;
                }
                else
                {
                    return null;
                }
            }
        }

        public VisitType Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save visitType without setted Id");
            }

            using (Leaf.Datalayers.VisitType.DataLayer dl = new Leaf.Datalayers.VisitType.DataLayer(_config))
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
