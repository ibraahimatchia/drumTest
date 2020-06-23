using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DAL;
using DAL.DBContext;
using DAL.Models;
using EvaluationDrum.Services;

namespace EvaluationDrum.Controllers
{
    public class DrumController : ApiController
    {
        private readonly DrumService drumService;

        public DrumController(DrumService drumService)
        {
            this.drumService = drumService;
        }
        //public IEnumerable<Drum> Get()
        //{
        //    using (EvaluationDrumDbContext context = new EvaluationDrumDbContext())
        //    {
        //        return context.Drum.ToList();
        //    }
        //}

        public Drum Get(int id)
        {
            using (EvaluationDrumDbContext context = new EvaluationDrumDbContext())
            {
                return context.Drum.FirstOrDefault(d => d.Id == id);
            }
        }

        //public JsonResult<List<Drum>> GetAllDrums()
        //{
        //    using (EvaluationDrumDbContext context = new EvaluationDrumDbContext())
        //    {
        //        return Json<List<Drum>>(context.Drum.ToList());
        //    }
        //}

        [HttpGet]
        public IHttpActionResult GetAllDrums()
        {
            IEnumerable<Drum> drums = drumService.GetDrums(); 
            return Ok(drums);

        }
    }
}
