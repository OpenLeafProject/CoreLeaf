﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class PaymentMethod
    {
        private int id;
        private string description;
        private string code;
        private DateTime creationDate;

        public int Id { get => id; }
        public string Description { get => description; set => description = value; }
        public string Code { get => code; set => code = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
    }
}
