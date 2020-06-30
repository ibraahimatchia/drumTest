using EvalDrum.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EvalDrum.DAL.Models;
using Common.Shared.ExceptionHandling;

namespace EvalDrum.API.Services
{
    public class DrumsService
    {
        private readonly EvalDrumContext _dbContext;

        public DrumsService()
        {
           this._dbContext = new EvalDrumContext();
        }

        public DrumsService(EvalDrumContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<DrumDetailDto> GetDrums()
        {
            var drums = from d in _dbContext.Drums
                        select new DrumDetailDto()
                        {
                            Id = d.Id,
                            DrumNumber = d.DrumNumber,
                            CreatedOn = d.CreatedOn,
                            Latitude = d.Latitude,
                            Longitude = d.Longitude,
                            InPositionSince = d.InPositionSince,
                            Site = d.Site.Name,
                            Status = d.Status.Status_name,
                            DrumManager = d.DrumManager.Name,
                            LastStatusUpdate = d.LastStatusUpdate
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
                DrumManager = d.DrumManager.Name,
                LastStatusUpdate = d.LastStatusUpdate
            }).SingleOrDefault(d => d.Id == id);

            return drum;
        }

        public DrumDetailDto UpdateDrum(int drumId, DrumCreateDto drumDetail)
        {
            if (!DrumExists(drumId))
            {
                throw new BadRequestException($"Drum with Id {drumId} doesn't exist.", BadRequestException.DRUM_DOESNT_EXISTS);
            }
            else
            {
                //get drum manager, site and status id
                DrumManager dm = _dbContext.DrumManagers.FirstOrDefault(d => d.Name == drumDetail.DrumManager);
                Site st = _dbContext.Sites.FirstOrDefault(s => s.Name == drumDetail.Site);
                Status sts = _dbContext.Status.FirstOrDefault(stt => stt.Status_name == drumDetail.Status);
                if (dm == null)
                {
                    throw new BadRequestException($"Drum Manager with name {drumDetail.DrumManager} doesn't exist.", BadRequestException.WRONG_DRUM_MANAGER_NAME);
                }
                if (st == null)
                {
                    throw new BadRequestException($"Drum Manager with name {drumDetail.Site} doesn't exist.", BadRequestException.WRONG_SITE_NAME);
                }
                if (sts == null)
                {
                    throw new BadRequestException($"Drum Manager with name {drumDetail.Status} doesn't exist.", BadRequestException.WRONG_STATUS_NAME);
                }

                Drum drum = _dbContext.Drums.FirstOrDefault(d => d.Id.Equals(drumId));

                drum.DrumNumber = drumDetail.DrumNumber;
                drum.DrumManager_Id = dm.Id;
                drum.Latitude = drumDetail.Latitude;
                drum.Longitude = drumDetail.Longitude;
                drum.InPositionSince = drumDetail.InPositionSince;
                drum.Site_Id = st.Id;
                drum.Status_Id = sts.Id;
                drum.LastStatusUpdate = DateTime.UtcNow;

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return new DrumDetailDto
                {
                    Id = drum.Id,
                    DrumNumber = drum.DrumNumber,
                    CreatedOn = drum.CreatedOn,
                    InPositionSince = drum.InPositionSince,
                    Latitude = drum.Latitude,
                    Longitude = drum.Longitude,
                    DrumManager = drum.DrumManager.Name,
                    Site = drum.Site.Name,
                    Status = drum.Status.Status_name,
                    LastStatusUpdate = drum.LastStatusUpdate
                };
            }
        }

        public DrumDetailDto CreatDrum(string drumNumber, string drumManagerName, string siteName, string statusName, double? latitude, double? longitude)
        {
            if (_dbContext.Drums.FirstOrDefault(d => d.DrumNumber == drumNumber) != null)
            {
                throw new BadRequestException($"Drum with drumNumber {drumNumber} already exist.", BadRequestException.DUPLICATE_DRUM_NB);
            }
            else
            {
                //get drum manager, site and status id
                DrumManager dm = _dbContext.DrumManagers.FirstOrDefault(d => d.Name == drumManagerName);
                Site st = _dbContext.Sites.FirstOrDefault(s => s.Name == siteName);
                Status sts = _dbContext.Status.FirstOrDefault(stt => stt.Status_name == statusName);
                if (dm == null)
                {
                    throw new BadRequestException($"Drum Manager with name {drumManagerName} doesn't exist.", BadRequestException.WRONG_DRUM_MANAGER_NAME);
                }
                if (st == null)
                {
                    throw new BadRequestException($"Site with name {siteName} doesn't exist.", BadRequestException.WRONG_SITE_NAME);
                }
                if (sts == null)
                {
                    throw new BadRequestException($"Status with name {statusName} doesn't exist.", BadRequestException.WRONG_STATUS_NAME);
                }

                Drum createDrum = new Drum()
                {
                    DrumNumber = drumNumber,
                    DrumManager_Id = dm.Id,
                    Site_Id = st.Id,
                    Status_Id = sts.Id,
                    CreatedOn = DateTime.UtcNow,
                    InPositionSince = DateTime.UtcNow,
                    Latitude = latitude,
                    Longitude = longitude,
                    LastStatusUpdate = DateTime.UtcNow
                };
                Drum drum = _dbContext.Drums.Add(createDrum);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return new DrumDetailDto
                {
                    Id = drum.Id,
                    DrumNumber = drum.DrumNumber,
                    CreatedOn = drum.CreatedOn,
                    InPositionSince = drum.InPositionSince,
                    Latitude = drum.Latitude,
                    Longitude = drum.Longitude,
                    DrumManager = drum.DrumManager.Name,
                    Site = drum.Site.Name,
                    Status = drum.Status.Status_name,
                    LastStatusUpdate = drum.LastStatusUpdate
                };
            }
        }

        public void DrumDeleteById(int id)
        {
            Drum drum = _dbContext.Drums.FirstOrDefault(d => d.Id == id);
            if (drum == null) throw new NotFoundException<Drum>(id);

            _dbContext.Drums.Remove(drum);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        public void DrumDeleteByDrumNumber(string drumNumber)
        {
            Drum drum = _dbContext.Drums.FirstOrDefault(d => d.DrumNumber == drumNumber);
            if (drum == null) throw new NotFoundException<Drum>(drumNumber);

            _dbContext.Drums.Remove(drum);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        private bool DrumExists(int id)
        {
            return _dbContext.Drums.Count(e => e.Id == id) > 0;
        }
    }
}