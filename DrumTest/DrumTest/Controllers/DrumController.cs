using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.DBContext;
using DAL.Models;

namespace EvaluationDrum.Controllers
{
    public class DrumController : ApiController
    {
        public IEnumerable<Drum> Get()
        {
            using (EvaluationDrumDbContext context = new EvaluationDrumDbContext())
            {
                return context.Drum.ToList();
            }
        }

        public Drum Get(int id)
        {
            using (EvaluationDrumDbContext context = new EvaluationDrumDbContext())
            {
                return context.Drum.FirstOrDefault(d => d.Id == id);
            }
        }
    }
}
