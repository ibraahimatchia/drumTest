using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Statuses")]
    public class Statuses
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
