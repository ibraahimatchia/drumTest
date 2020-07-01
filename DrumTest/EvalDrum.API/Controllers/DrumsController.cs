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

        public DrumsController(DrumsService service)
        {
            _drumService = service;
        }

        // GET: api/Drums
        public IHttpActionResult GetDrums()
        {
            IEnumerable<DrumDetailDto> drums = _drumService.GetDrums();
            return Ok(drums);
        }

        // GET: api/Drums/5
        [ResponseType(typeof(DrumDetailDto))]
        public IHttpActionResult GetDrum(int id)
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
        [ResponseType(typeof(DrumCreateDto))]
        public IHttpActionResult PutDrum(int id, DrumCreateDto drum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DrumDetailDto drumUp = _drumService.UpdateDrum(id, drum);

            return Ok(drumUp);
        }

        [Authorize]
        // POST: api/Drums
        [ResponseType(typeof(DrumCreateDto))]
        public IHttpActionResult PostDrum(DrumCreateDto drum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DrumDetailDto CreateDrum =_drumService.CreatDrum(drum.DrumNumber, drum.DrumManager, drum.Site, drum.Status,drum.Latitude, drum.Longitude);

            return Ok(CreateDrum);
        }

        [Authorize]
        // DELETE: api/Drums/5
        public IHttpActionResult DeleteDrum(int id)
        {
            _drumService.DrumDeleteById(id);
            return Ok();
        }

        [Authorize]
        // DELETE: api/Drums/sample string 2
        public IHttpActionResult DeleteDrum(string drumNumber)
        {
            _drumService.DrumDeleteByDrumNumber(drumNumber);
            return Ok();
        }
    }
}