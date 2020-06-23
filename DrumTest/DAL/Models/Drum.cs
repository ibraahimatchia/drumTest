using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Drums")]
    public class Drum
    {
        public int Id { get; set; }
        public string DrumNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? InPositionSince { get; set; }

        [ForeignKey("Site")]
        public int? Site_Id { get; set; }
        public Sites Site { get; set; }

        public DateTime? LastStatusUpdate { get; set; }
        [ForeignKey("Status")]
        public int? Status_Id { get; set; }
        public Statuses Status { get; set; }

        [ForeignKey("DrumManager")]
        public int? DrumManager_Id { get; set; }
        public DrumManager DrumManager { get; set; }

    }
}
