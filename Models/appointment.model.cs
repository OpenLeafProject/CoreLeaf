using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Appointment
    {
        private int id;
        private DateTime datetime;
        private int forced;
        private int allowInvoice;
        private int price;
        private int duration;
        private int visitmode;
        private string note;
        private Patient patient;
        private VisiType visitType;
        private VisitMode visitMode;

        public int Id { get => id; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public int Forced { get => forced; set => forced = value; }
        public int AllowInvoice { get => allowInvoice; set => allowInvoice = value; }
        public int Price { get => price; set => price = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Visitmode { get => visitmode; set => visitmode = value; }
        public VisitMode VisitMode { get => visitMode; set => visitMode = value; }
        public string Note { get => note; set => note = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public VisiType VisitType { get => visitType; set => visitType = value; }
    }
}
