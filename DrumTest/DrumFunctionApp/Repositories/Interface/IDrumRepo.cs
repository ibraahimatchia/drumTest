using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvalDrum.DAL;
using EvalDrum.DAL.Models;

namespace DrumFunctionApp.Repositories.Interface
{
    public interface IDrumRepo
    {
        List<Drum> GetDrums();
        void RemoveDrum();
        void SaveChanges();
    }
}
