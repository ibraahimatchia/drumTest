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
    public class StatusController : ApiController
    {
        private StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            this._statusService = statusService;
        }

        // GET: api/Status
        public IHttpActionResult GetStatus()
        {
            IEnumerable<Status> statuses = _statusService.GetStatus();
            return Ok(statuses);
        }

        // GET: api/Status/5
        [ResponseType(typeof(Status))]
        public IHttpActionResult GetStatus(int id)
        {
            Status status = _statusService.GetStatusById(id);
            if (status == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(status);
        }

        [Authorize]
        // PUT: api/Status/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatus(int id, Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Status StatusUp = _statusService.UpdateStatus(id, status);

            return Ok(StatusUp);
        }

        [Authorize]
        // POST: api/Status
        [ResponseType(typeof(Status))]
        public IHttpActionResult PostStatus(Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Status CreateStatus = _statusService.CreatStatus(status);

            return Ok(CreateStatus);
        }

        [Authorize]
        // DELETE: api/Status/5
        public IHttpActionResult DeleteStatus(int id)
        {
            _statusService.DeleteStatusById(id);
            return Ok();
        }

        [Authorize]
        // DELETE: api/Status?statusName = xxxx
        public IHttpActionResult DeleteStatus(string statusName)
        {
            _statusService.DeleteStatusByName(statusName);
            return Ok();
        }
    }
}