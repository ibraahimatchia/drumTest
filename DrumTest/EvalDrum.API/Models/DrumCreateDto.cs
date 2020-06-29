using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvalDrum.API.Models
{
    public class DrumCreateDto
    {
        public string DrumNumber { get; set; }
        public string Site { get; set; }
        public string Status { get; set; }
        public string DrumManager { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? InPositionSince { get; set; }
    }
}