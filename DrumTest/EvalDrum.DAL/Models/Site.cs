﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalDrum.DAL.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Number { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
    }
}
