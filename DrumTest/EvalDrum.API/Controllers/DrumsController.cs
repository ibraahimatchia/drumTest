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
using EvalDrum.DAL.Models;

namespace EvalDrum.API.Controllers
{
    public class DrumsController : ApiController
    {
        private EvalDrumContext db = new EvalDrumContext();

        // GET: api/Drums
        public IQueryable<DrumDto> GetDrums()
        {
            var drums = from d in db.Drums
                        select new DrumDto()
                        {
                            Id = d.Id,
                            Site = d.Site.Name,
                            Status = d.Status.Status_name,
                            DrumManager = d.DrumManager.Name
                        };
            return drums;
        }

        // GET: api/Drums/5
        [ResponseType(typeof(DrumDetailDto))]
        public async Task<IHttpActionResult> GetDrum(int id)
        {
            var drum = await db.Drums.Include(d => d.Site).Include(d => d.Status).Include(d =>d.DrumManager).Select(d => new DrumDetailDto()
            {
                Id = d.Id,
                DrumNumber = d.DrumNumber,
                CreatedOn = d.CreatedOn,
                Latitude = d.Latitude,
                Longitude = d.Longitude,
                InPositionSince = d.InPositionSince,
                Site = d.Site.Name,
                Status = d.Status.Status_name,
                DrumManager = d.DrumManager.Name
            }).SingleOrDefaultAsync(d => d.Id == id);

            if (drum == null)
            {
                return NotFound();
            }
            return Ok(drum);
        }

        // PUT: api/Drums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDrum(int id, Drum drum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drum.Id)
            {
                return BadRequest();
            }

            db.Entry(drum).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

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
                DrumManager = drum.DrumManager.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = drum.Id }, dto);
        }

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
        }
    }
}