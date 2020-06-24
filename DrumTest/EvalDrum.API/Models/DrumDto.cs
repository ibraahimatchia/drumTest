using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvalDrum.API.Models
{
    public class DrumDto
    {
        public int Id { get; set; }
        public string DrumNumber { get; set; }
        public string Site { get; set; }
        public string Status { get; set; }
        public string DrumManager { get; set; }
    }
}