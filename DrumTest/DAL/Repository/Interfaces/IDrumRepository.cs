using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IDrumRepository
    {
        IEnumerable<Drum> GetDrums();
        Drum GetDrum(int drumId);
        void AddDrum(Drum drum);
        void DeleteDrum(Drum drum);
        void SaveChanges();
    }
}
