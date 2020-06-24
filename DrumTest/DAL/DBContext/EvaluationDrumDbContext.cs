using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class EvaluationDrumDbContext : DbContext
    {
        public DbSet<Drum> Drum { get; set; }

        public DbSet<DrumManager> DrumManager { get; set; }

        public DbSet<Sites> Site { get; set; }

        public DbSet<Statuses> Status { get; set; }
    }
}
