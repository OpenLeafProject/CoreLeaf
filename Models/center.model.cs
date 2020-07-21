using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        private readonly IConfiguration _config;

        public int Id { get => id; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Nif { get => nif; set => nif = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Pc { get => pc; set => pc = value; }

        public Center(IConfiguration config)
        {
            _config = config;
        }

        public Center(int id, IConfiguration config)
        {
            _config = config;
            loadCenterById(id);
        }

        public Center(string code, IConfiguration config)
        {
            _config = config;
            loadCenterByCode(code);
        }

        private void loadCenterById(int id)
        {
            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadCenterById(id);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Name = dt.Rows[0]["name"].ToString();
                    Nif = dt.Rows[0]["nif"].ToString();
                    Address = dt.Rows[0]["address"].ToString();
                    City = dt.Rows[0]["city"].ToString();
                    Pc = dt.Rows[0]["pc"].ToString();
                }
                else
                {
                    throw new System.NullReferenceException("Patient not found");
                }
            }
        }

        private void loadCenterByCode(string code)
        {
            using (Leaf.Datalayers.Center.DataLayer dl = new Leaf.Datalayers.Center.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.loadCenterByCode(code);
                if (dt.Rows.Count == 1)
                {
                    this.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    Code = dt.Rows[0]["code"].ToString();
                    Name = dt.Rows[0]["name"].ToString();
                    Nif = dt.Rows[0]["nif"].ToString();
                    Address = dt.Rows[0]["address"].ToString();
                    City = dt.Rows[0]["city"].ToString();
                    Pc = dt.Rows[0]["pc"].ToString();
                }
                else
                {
                    throw new System.NullReferenceException("Patient not found");
                }
            }
        }
    }
 }
