using DAL.DBContext;
using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DrumRepository : IDrumRepository
    {
        private readonly EvaluationDrumDbContext dbcontext;

        public DrumRepository(EvaluationDrumDbContext context)
        {
            this.dbcontext = context;
        }
        public void AddDrum(Drum drum)
        {
            dbcontext.Drum.Add(drum);
        }

        public void DeleteDrum(Drum drum)
        {
            dbcontext.Drum.Remove(drum);
        }

        public Drum GetDrum(int drumId)
        {
            return dbcontext.Drum.FirstOrDefault(d => d.Id.Equals(drumId));
        }

        public IEnumerable<Drum> GetDrums()
        {
            return dbcontext.Drum.ToList();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
