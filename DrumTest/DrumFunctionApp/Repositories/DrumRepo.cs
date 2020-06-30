using DrumFunctionApp.Repositories.Interface;
using EvalDrum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrumFunctionApp.Repositories
{
    public class DrumRepo : IDrumRepo
    {
        private readonly EvalDrumContext _context;

        public DrumRepo(EvalDrumContext context)
        {
            this._context = context;
        }
        public List<Drum> GetDrums()
        {
            throw new NotImplementedException();
        }

        public void RemoveDrum()
        {
            string sqlCommand = @"DELETE FROM [dbo].[Drums] 
                                    WHERE InPositionSince < DATEADD(year,-2,GETDATE())";

            _context.Database.ExecuteSqlCommand(sqlCommand);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
