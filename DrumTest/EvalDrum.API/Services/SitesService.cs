using Common.Shared.ExceptionHandling;
using EvalDrum.API.Models;
using EvalDrum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvalDrum.API.Services
{
    public class SitesService
    {
        private readonly EvalDrumContext _dbContext;

        public SitesService()
        {
            this._dbContext = new EvalDrumContext();
        }

        public IEnumerable<Site> GetSites()
        {
            var sites = _dbContext.Sites;
            return sites;
        }

        public Site GetSitesById(int id)
        {
            var site = _dbContext.Sites.SingleOrDefault(s => s.Id == id);

            return site;
        }


        public Site UpdateSite(int siteId, Site siteDetail)
        {
            if (!SiteExists(siteId))
            {
                throw new BadRequestException($"Drum with Id {siteId} doesn't exist.", BadRequestException.DRUM_DOESNT_EXISTS);
            }
            else
            {
                Site site = _dbContext.Sites.FirstOrDefault(s => s.Id.Equals(siteId));

                site.Name = siteDetail.Name;
                site.Street = siteDetail.Street;
                site.Street2 = siteDetail.Street2;
                site.Street3 = siteDetail.Street3;
                site.PostalCode = siteDetail.PostalCode;
                site.City = siteDetail.City;
                site.Country = siteDetail.Country;
                site.Number = siteDetail.Number;
                site.Tel = siteDetail.Tel;
                site.Fax = siteDetail.Fax;
                site.EMail = siteDetail.EMail;


                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return new Site
                {
                    Id = site.Id,
                    Name = site.Name,
                    Street = site.Street,
                    Street2 = site.Street2,
                    Street3 = site.Street3,
                    PostalCode = site.PostalCode,
                    City = site.City,
                    Country = site.Country,
                    Number = site.Number,
                    Tel = site.Tel,
                    Fax = site.Fax,
                    EMail = site.EMail
            };
            }
        }

        public Site CreatSite(Site createSite)
        {
            if (_dbContext.Sites.FirstOrDefault(s => s.Name == createSite.Name) != null)
            {
                throw new BadRequestException($"Site with name {createSite.Name} already exist.", BadRequestException.DUPLICATE_SITE_NAME);
            }
            else
            {
                Site site = _dbContext.Sites.Add(createSite);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return site;
            }
        }

        public void DeleteSiteById(int id)
        {
            Site site = _dbContext.Sites.FirstOrDefault(s => s.Id == id);
            if (site == null) throw new NotFoundException<Site>(id);

            _dbContext.Sites.Remove(site);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        public void DeleteSiteBySiteName(string siteName)
        {
            Site site = _dbContext.Sites.FirstOrDefault(s => s.Name == siteName);
            if (site == null) throw new NotFoundException<Drum>(siteName);

            _dbContext.Sites.Remove(site);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        private bool SiteExists(int id)
        {
            return _dbContext.Sites.Count(s => s.Id == id) > 0;
        }
    }
}