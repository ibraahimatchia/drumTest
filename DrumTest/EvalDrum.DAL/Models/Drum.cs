using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalDrum.DAL.Models
{
    public class Drum
    {
        public int Id { get; set; }
        public string DrumNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? InPositionSince { get; set; }
        //Foreign Key
        [ForeignKey("Site")]
        public int? Site_Id { get; set; }
        public virtual Site Site { get; set; }
        public DateTime? LastStatusUpdate { get; set; }
        //Foreign Key
        [ForeignKey("Status")]
        public int? Status_Id { get; set; }
        public Status Status { get; set; }
        //Foreign Key
        [ForeignKey("DrumManager")]
        public int? DrumManager_Id { get; set; }

        public DrumManager DrumManager { get; set; }
    }
}
