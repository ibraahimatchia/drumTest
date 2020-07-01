using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalDrum.DAL.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Status_name { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
