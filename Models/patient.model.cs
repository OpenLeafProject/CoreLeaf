using System;
using System.Collections.Generic;
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
        private string pgonealt;
        private string address;
        private string city;
        private string pc;
        private int sex;
        private string note;
        private DateTime lastAccess;
        private string email;
        private Center center;

        public int Nhc { get => nhc; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Pgonealt { get => pgonealt; set => pgonealt = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Pc { get => pc; set => pc = value; }
        public int Sex { get => sex; set => sex = value; }
        public string Note { get => note; set => note = value; }
        public DateTime LastAccess { get => lastAccess; set => lastAccess = value; }
        public string Email { get => email; set => email = value; }
        public Center Center { get => center; set => center = value; }

        public Patient(int nhc)
        {
            loadPatientbyNHC(nhc);
        }

        public Patient(string dni)
        {
            loadPatientbyDNI(dni);
        }

        private void loadPatientbyNHC(int nhc)
        {

        }

        private void loadPatientbyDNI(string dni)
        {

        }
    }
}
