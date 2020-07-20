using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Report
    {
        private int id;
        private string content;
        private DateTime creationDate;
        private DateTime signDate;
        private Patient patient;
        private Appointment appointment;

        public int Id { get => id; }
        public string Content { get => content; set => content = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime SignDate { get => signDate; set => signDate = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Appointment Appointment { get => appointment; set => appointment = value; }
    }
}
