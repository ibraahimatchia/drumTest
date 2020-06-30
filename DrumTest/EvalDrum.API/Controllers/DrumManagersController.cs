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
using EvalDrum.API.Services;
using EvalDrum.DAL.Models;

namespace EvalDrum.API.Controllers
{
    public class DrumManagersController : ApiController
    {
        private DrumManagersService _drumManagerService;

        public DrumManagersController()
        {
            this._drumManagerService = new DrumManagersService();
        }

        // GET: api/DrumManagers
        public IHttpActionResult GetDrumManagers()
        {
            IEnumerable<DrumManager> drumManagers = _drumManagerService.GetDrumManagers();
            return Ok(drumManagers);
        }

        // GET: api/DrumManagers/5
        [ResponseType(typeof(DrumManager))]
        public IHttpActionResult GetDrumManager(int id)
        {
            DrumManager drumManager = _drumManagerService.GetDrumManagerById(id);
            if (drumManager == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(drumManager);
        }

        [Authorize]
        // PUT: api/DrumManagers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrumManager(int id, DrumManager drumManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DrumManager drumManagerUp = _drumManagerService.UpdateDrumManager(id, drumManager);

            return Ok(drumManagerUp);

        }

        [Authorize]
        // POST: api/DrumManagers
        [ResponseType(typeof(DrumManager))]
        public IHttpActionResult PostDrumManager(DrumManager drumManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DrumManager drumManagerCreate = _drumManagerService.CreatDrumManager(drumManager);

            return Ok(drumManagerCreate);

        }

        [Authorize]
        // DELETE: api/DrumManagers/5
        public IHttpActionResult DeleteDrumManager(int id)
        {
            _drumManagerService.DeleteDrumManagerById(id);
            return Ok();
        }

        [Authorize]
        // DELETE: api/DrumManagers?drumManagerName=xxxxx
        public IHttpActionResult DeleteDrumManager(string drumManagerName)
        {
            _drumManagerService.DeleteDrumManagerByName(drumManagerName);
            return Ok();
        }
    }
}