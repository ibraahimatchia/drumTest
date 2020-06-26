using EvalDrum.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using EvalDrum.DAL.Models;
using System.Data.Entity.Infrastructure;

namespace EvalDrum.API.Services
{
    public class DrumsService
    {
        private readonly EvalDrumContext _dbContext;

        public DrumsService()
        {
           this._dbContext = new EvalDrumContext();
        }

        public IEnumerable<DrumDto> GetDrums()
        {
            var drums = from d in _dbContext.Drums
                        select new DrumDto()
                        {
                            Id = d.Id,
                            Site = d.Site.Name,
                            Status = d.Status.Status_name,
                            DrumManager = d.DrumManager.Name,
                            DrumNumber = d.DrumNumber
                        };
            return drums;
        }

        public DrumDetailDto GetDrumsById(int id)
        {
            var drum = _dbContext.Drums.Include(d => d.Site).Include(d => d.Status).Include(d => d.DrumManager).Select(d => new DrumDetailDto()
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
            }).SingleOrDefault(d => d.Id == id);

            return drum;
        }

        public Drum UpdateDrum(int drumId, Drum drumDetail)
        {
            if (!DrumExists(drumId))
            {
                return null;
            }
            else
            {
                Drum drum = _dbContext.Drums.FirstOrDefault(d => d.Id.Equals(drumId));

                drum.DrumNumber = drumDetail.DrumNumber;
                drum.DrumManager_Id = drumDetail.DrumManager_Id;
                drum.Latitude = drumDetail.Latitude;
                drum.Longitude = drumDetail.Longitude;
                drum.InPositionSince = drumDetail.InPositionSince;
                drum.Site_Id = drumDetail.Site_Id;
                drum.Status_Id = drumDetail.Status_Id;

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
                return drum;
            }
        }

        private bool DrumExists(int id)
        {
            return _dbContext.Drums.Count(e => e.Id == id) > 0;
        }
    }
}