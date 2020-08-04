using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class UserProfile
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

        public UserProfile(IConfiguration config)
        {
            _config = config;
        }

        public UserProfile(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public UserProfile(string code, IConfiguration config)
        {
            _config = config;
            LoadByCode(code);
        }

        public UserProfile(Dictionary<string, string> values, IConfiguration config)
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
            CreationDate = values["creationDate"] != "" ? DateTime.Parse(values["creationDate"]) : DateTime.Now;

        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.UserProfile.DataLayer dl = new Leaf.Datalayers.UserProfile.DataLayer(_config))
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
                    throw new System.NullReferenceException("UserProfile not found");
                }
            }
        }

        private void LoadByCode(string code)
        {
            using (Leaf.Datalayers.UserProfile.DataLayer dl = new Leaf.Datalayers.UserProfile.DataLayer(_config))
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
                    throw new System.NullReferenceException("UserProfile not found");
                }
            }
        }

        public UserProfile Create()
        {
            using (Leaf.Datalayers.UserProfile.DataLayer dl = new Leaf.Datalayers.UserProfile.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create userProfile with setted Id");
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

        public UserProfile Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save user userProfile without setted Id");
            }

            using (Leaf.Datalayers.UserProfile.DataLayer dl = new Leaf.Datalayers.UserProfile.DataLayer(_config))
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
