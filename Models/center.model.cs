using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Center
    {
        private int id;
        private string code;
        private string name;
        private string nif;
        private string address;
        private string city;
        private string pc;
        private DateTime creationDate;

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Nif { get => nif; set => nif = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Pc { get => pc; set => pc = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public Center(IConfiguration config)
        {
            _config = config;
        }

        public Center(int id, IConfiguration config)
        {
            _config = config;
            LoadById(id);
        }

        public Center(string code, IConfiguration config)
        {
            _config = config;
            LoadByCode(code);
        }

        public Center(Dictionary<string, string> values, IConfiguration config)
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
            Name = values["name"];
            Nif = values["nif"];
            Address = values["address"];
            City = values["city"];
            Pc = values["pc"];
            CreationDate = DateTime.Parse(values["creationDate"]);

        }

        private void LoadById(int id)
        {
            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Name = dt.Rows[0]["name"].ToString();
                    Nif = dt.Rows[0]["nif"].ToString();
                    Address = dt.Rows[0]["address"].ToString();
                    City = dt.Rows[0]["city"].ToString();
                    Pc = dt.Rows[0]["pc"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationdate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("Center not found");
                }
            }
        }

        private void LoadByCode(string code)
        {
            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadByCode(code);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Name = dt.Rows[0]["name"].ToString();
                    Nif = dt.Rows[0]["nif"].ToString();
                    Address = dt.Rows[0]["address"].ToString();
                    City = dt.Rows[0]["city"].ToString();
                    Pc = dt.Rows[0]["pc"].ToString();
                    CreationDate = DateTime.Parse(dt.Rows[0]["creationdate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("Center not found");
                }
            }
        }

        public Center Create()
        {
            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
            {

                if (this.Id > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create center with setted Id");
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

        public Center Save()
        {
            if (!(this.Id > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save center without setted Id");
            }

            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
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
