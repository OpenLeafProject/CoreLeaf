using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Invoice
    {
        private int id;
        private Double ammount;
        private Double iva;
        private DateTime invoiceDate;
        private DateTime paymentDate;
        private Patient patient;
        private Appointment appointment;
        private Center center;
        private PaymentMethod paymentMethod;

        public int Id { get => id; }
        public double Ammount { get => ammount; set => ammount = value; }
        public double Iva { get => iva; set => iva = value; }
        public DateTime InvoiceDate { get => invoiceDate; set => invoiceDate = value; }
        public DateTime PaymentDate { get => paymentDate; set => paymentDate = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Appointment Appointment { get => appointment; set => appointment = value; }
        public Center Center { get => center; set => center = value; }
        public PaymentMethod PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
    }
}
