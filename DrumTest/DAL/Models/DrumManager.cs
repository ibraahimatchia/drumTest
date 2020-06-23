using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("DrumManagers")]
    public class DrumManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
    }
}
