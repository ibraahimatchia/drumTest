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
    public class SitesController : ApiController
    {
        private SitesService _siteService;

        public SitesController()
        {
            this._siteService = new SitesService();
        }

        // GET: api/Sites
        public IHttpActionResult GetSites()
        {
            IEnumerable<Site> sites = _siteService.GetSites();
            return Ok(sites);
        }

        // GET: api/Sites/5
        [ResponseType(typeof(Site))]
        public IHttpActionResult GetSite(int id)
        {
            Site site = _siteService.GetSitesById(id);
            if (site == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(site);
        }

        [Authorize]
        // PUT: api/Sites/5
        [ResponseType(typeof(Site))]
        public IHttpActionResult PutSite(int id, Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Site SiteUp = _siteService.UpdateSite(id, site);

            return Ok(SiteUp);
        }

        [Authorize]
        // POST: api/Sites
        [ResponseType(typeof(Site))]
        public IHttpActionResult PostSite(Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Site CreateSite = _siteService.CreatSite(site);

            return Ok(CreateSite);
        }

        [Authorize]
        // DELETE: api/Sites/5
        public IHttpActionResult DeleteSite(int id)
        {
            _siteService.DeleteSiteById(id);
            return Ok();
        }

        [Authorize]
        // DELETE: api/Sites/SERVAL_LOGISTIC_SITE_BORDEAUX
        public IHttpActionResult DeleteSite(string siteName)
        {
            _siteService.DeleteSiteBySiteName(siteName);
            return Ok();
        }
    }
}