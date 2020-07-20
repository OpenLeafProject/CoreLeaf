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

        public int Id { get => id; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Nif { get => nif; set => nif = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string Pc { get => pc; set => pc = value; }
    }
}
