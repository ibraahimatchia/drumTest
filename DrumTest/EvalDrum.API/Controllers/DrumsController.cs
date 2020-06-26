using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EvalDrum.API.Models;
using EvalDrum.API.Services;
using EvalDrum.DAL.Models;

namespace EvalDrum.API.Controllers
{
    public class DrumsController : ApiController
    {
        private readonly DrumsService _drumService;

        public DrumsController()
        {
            _drumService = new DrumsService();
        }

        // GET: api/Drums
        public IHttpActionResult GetDrums()
        {
            IEnumerable<DrumDto> drums = _drumService.GetDrums();
            return Ok(drums);
        }

        // GET: api/Drums/5
        [ResponseType(typeof(DrumDetailDto))]
        public IHttpActionResult GetDrumAsync(int id)
        {
            DrumDetailDto drum = _drumService.GetDrumsById(id);
            if (drum == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(drum);
        }


        [Authorize]
        // PUT: api/Drums/5
        [ResponseType(typeof(Drum))]
        public IHttpActionResult PutDrum(int id, Drum drum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Drum drumUp = _drumService.UpdateDrum(id, drum);

            if (drumUp == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /*[Authorize]
        // POST: api/Drums
        [ResponseType(typeof(DrumDto))]
        public async Task<IHttpActionResult> PostDrum(Drum drum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drums.Add(drum);
            await db.SaveChangesAsync();

            db.Entry(drum).Reference(d => d.Site).Load();
            db.Entry(drum).Reference(d => d.Status).Load();
            db.Entry(drum).Reference(d => d.DrumManager).Load();

            var dto = new DrumDto()
            {
                Id = drum.Id,
                Site = drum.Site.Name,
                Status = drum.Status.Status_name,
                DrumManager = drum.DrumManager.Name,
                DrumNumber = drum.DrumNumber
            };

            return CreatedAtRoute("DefaultApi", new { id = drum.Id }, dto);
        }

        [Authorize]
        // DELETE: api/Drums/5
        [ResponseType(typeof(Drum))]
        public async Task<IHttpActionResult> DeleteDrum(int id)
        {
            Drum drum = await db.Drums.FindAsync(id);
            if (drum == null)
            {
                return NotFound();
            }

            db.Drums.Remove(drum);
            await db.SaveChangesAsync();

            return Ok(drum);
        }

        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrumExists(int id)
        {
            return db.Drums.Count(e => e.Id == id) > 0;
        }*/
    }
}