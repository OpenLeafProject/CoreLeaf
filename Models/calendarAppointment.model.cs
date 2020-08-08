using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class CalendarAppointment
    {

        private int id;
        private string title;
        private DateTime start;
        private DateTime end;
        private string color;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public string Color { get => color; set => color = value; }
    }
}
