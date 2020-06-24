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
using EvalDrum.DAL.Models;

namespace EvalDrum.API.Controllers
{
    public class DrumManagersController : ApiController
    {
        private EvalDrumContext db = new EvalDrumContext();

        // GET: api/DrumManagers
        public IQueryable<DrumManager> GetDrumManagers()
        {
            return db.DrumManagers;
        }

        // GET: api/DrumManagers/5
        [ResponseType(typeof(DrumManager))]
        public async Task<IHttpActionResult> GetDrumManager(int id)
        {
            DrumManager drumManager = await db.DrumManagers.FindAsync(id);
            if (drumManager == null)
            {
                return NotFound();
            }

            return Ok(drumManager);
        }

        // PUT: api/DrumManagers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDrumManager(int id, DrumManager drumManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drumManager.Id)
            {
                return BadRequest();
            }

            db.Entry(drumManager).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrumManagerExists(id))
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

        // POST: api/DrumManagers
        [ResponseType(typeof(DrumManager))]
        public async Task<IHttpActionResult> PostDrumManager(DrumManager drumManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DrumManagers.Add(drumManager);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = drumManager.Id }, drumManager);
        }

        // DELETE: api/DrumManagers/5
        [ResponseType(typeof(DrumManager))]
        public async Task<IHttpActionResult> DeleteDrumManager(int id)
        {
            DrumManager drumManager = await db.DrumManagers.FindAsync(id);
            if (drumManager == null)
            {
                return NotFound();
            }

            db.DrumManagers.Remove(drumManager);
            await db.SaveChangesAsync();

            return Ok(drumManager);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrumManagerExists(int id)
        {
            return db.DrumManagers.Count(e => e.Id == id) > 0;
        }
    }
}