using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Patient
    {

        private int nhc;
        private string name;
        private string surname;
        private string lastname;
        private string dni;
        private string phone;
        private string phonealt;
        private string address;
        private string city;
        private string pc;
        private int sex;
        private string note;
        private DateTime lastAccess;
        private string email;
        private Center center;
        private DateTime bornDate;

        private readonly IConfiguration _config;

        public int Nhc { get => nhc; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Phone { get => phone; set => phone = value; }
        public string PhoneAlt { get => phonealt; set => phonealt = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Pc { get => pc; set => pc = value; }
        public int Sex { get => sex; set => sex = value; }
        public string Note { get => note; set => note = value; }
        public DateTime LastAccess { get => lastAccess; set => lastAccess = value; }
        public string Email { get => email; set => email = value; }
        public DateTime BornDate { get => bornDate; set => bornDate = value; }
        public Center Center { get => center; set => center = value; }

        public Patient(IConfiguration config)
        {
            _config = config;
        }

        public Patient(int nhc, IConfiguration config)
        {
            _config = config;
            loadPatientbyNHC(nhc);
        }

        public Patient(string dni, IConfiguration config)
        {
            _config = config;
            loadPatientbyDNI(dni);
        }

        public Patient(Dictionary<string, string> values, IConfiguration config)
        {
            _config = config;

            try
            {
                this.nhc = Int32.Parse(values["nhc"]);
                loadPatientbyNHC(this.Nhc);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                this.nhc = -1;
            }

            Name       = values["name"];
            Surname    = values["surname"];
            Lastname   = values["lastname"];
            Dni        = values["dni"];
            Phone      = values["phone"];
            PhoneAlt   = values["phoneAlt"];
            Address    = values["address"];
            City       = values["city"];
            Pc         = values["pc"];
            Sex        = Int32.Parse(values["sex"]);
            Note       = values["note"];
            LastAccess = DateTime.Parse(values["lastAccess"]);
            Email      = values["email"];
            Center     = new Center(Int32.Parse(values["center"]), _config);
            BornDate   = DateTime.Parse(values["bornDate"]);

        }

        private void loadPatientbyNHC(int nhc)
        {
            using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
            {
               System.Data.DataTable dt =  dl.GetpatientByNHC(nhc);
                if(dt.Rows.Count == 1)
                {
                    this.nhc = Int32.Parse(dt.Rows[0]["nhc"].ToString());
                    Name = dt.Rows[0]["name"].ToString();
                    Surname = dt.Rows[0]["surname"].ToString();
                    Lastname = dt.Rows[0]["Lastname"].ToString();
                    Dni = dt.Rows[0]["Dni"].ToString();
                    Phone = dt.Rows[0]["Phone"].ToString();
                    PhoneAlt = dt.Rows[0]["PhoneAlt"].ToString();
                    Address = dt.Rows[0]["Address"].ToString();
                    City = dt.Rows[0]["City"].ToString();
                    Pc = dt.Rows[0]["Pc"].ToString();
                    Sex = Int32.Parse(dt.Rows[0]["Sex"].ToString());
                    Note = dt.Rows[0]["Note"].ToString();
                    LastAccess = DateTime.Parse(dt.Rows[0]["LastAccess"].ToString());
                    Email = dt.Rows[0]["Email"].ToString();
                    Center = new Center(Int32.Parse(dt.Rows[0]["Centerid"].ToString()), _config);
                    BornDate = DateTime.Parse(dt.Rows[0]["borndate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("Patient not found");
                }
            }
        }

        private void loadPatientbyDNI(string dni)
        {
            using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
            {
                System.Data.DataTable dt = dl.GetpatientByDNI(dni);
                if (dt.Rows.Count == 1)
                {
                    this.nhc = Int32.Parse(dt.Rows[0]["nhc"].ToString());
                    Name = dt.Rows[0]["name"].ToString();
                    Surname = dt.Rows[0]["surname"].ToString();
                    Lastname = dt.Rows[0]["Lastname"].ToString();
                    Dni = dt.Rows[0]["Dni"].ToString();
                    Phone = dt.Rows[0]["Phone"].ToString();
                    PhoneAlt = dt.Rows[0]["PhoneAlt"].ToString();
                    Address = dt.Rows[0]["Address"].ToString();
                    City = dt.Rows[0]["City"].ToString();
                    Pc = dt.Rows[0]["Pc"].ToString();
                    Sex = Int32.Parse(dt.Rows[0]["Sex"].ToString());
                    Note = dt.Rows[0]["Note"].ToString();
                    LastAccess = DateTime.Parse(dt.Rows[0]["LastAccess"].ToString());
                    Email = dt.Rows[0]["Email"].ToString();
                    Center = new Center(Int32.Parse(dt.Rows[0]["Centerid"].ToString()), _config);
                    BornDate = DateTime.Parse(dt.Rows[0]["borndate"].ToString());
                }
                else
                {
                    throw new System.NullReferenceException("Patient not found");
                }
            }
        }

        public Patient createPatient()
        {
            using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
            {

                if(this.Nhc > 0)
                {
                    throw new System.IO.InvalidDataException("Cannot create patient with setted NHC");
                }

                bool correctdni = false ;
                if(Dni == "" || Dni == null)
                {
                    Random rand = new Random();
                    this.Dni = rand.Next().ToString();
                    correctdni = true;
                }

                if(dl.createPatient(this) == 1)
                {
                    loadPatientbyDNI(this.dni);
                    if(correctdni)
                    {
                        this.Dni = "";
                        savePatient();
                    }
                    return this;
                } else
                {
                    return null;
                }
            }
        }

        public Patient savePatient()
        {
            if (!(this.Nhc > 0))
            {
                throw new System.IO.InvalidDataException("Cannot save patient without setted NHC");
            }

            using (Leaf.Datalayers.Patient.DataLayer dl = new Leaf.Datalayers.Patient.DataLayer(_config))
            {
                if (dl.savePatient(this) == 1)
                {
                    loadPatientbyNHC(this.nhc);
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
