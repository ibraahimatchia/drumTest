using Common.Shared.ExceptionHandling;
using EvalDrum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvalDrum.API.Services
{
    public class DrumManagersService
    {
        private readonly EvalDrumContext _dbContext;

        public DrumManagersService()
        {
            this._dbContext = new EvalDrumContext();
        }

        public IEnumerable<DrumManager> GetDrumManagers()
        {
            var dm = _dbContext.DrumManagers;
            return dm;
        }

        public DrumManager GetDrumManagerById(int id)
        {
            var drumManager = _dbContext.DrumManagers.SingleOrDefault(dm => dm.Id == id);

            return drumManager;
        }

        public DrumManager UpdateDrumManager(int drumManagerId, DrumManager drumManagerDetail)
        {
            if (!DrumManagerExists(drumManagerId))
            {
                throw new BadRequestException($"Drum Manager with Id {drumManagerId} doesn't exist.", BadRequestException.DRUM_MANAGER_DOESNT_EXISTS);
            }
            else
            {
                DrumManager drumManager = _dbContext.DrumManagers.FirstOrDefault(dm => dm.Id.Equals(drumManagerId));

                drumManager.Name = drumManagerDetail.Name;
                drumManager.ContactEmail = drumManagerDetail.ContactEmail;

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return new DrumManager
                {
                    Id = drumManager.Id,
                    Name = drumManager.Name,
                    ContactEmail = drumManager.ContactEmail
                };
            }
        }

        public DrumManager CreatDrumManager(DrumManager createDrumManager)
        {
            if (_dbContext.DrumManagers.FirstOrDefault(dm => dm.Name == createDrumManager.Name) != null)
            {
                throw new BadRequestException($"Drum Manager with name {createDrumManager.Name} already exist.", BadRequestException.DUPLICATE_DRUM_MANAGER_NAME);
            }
            else
            {
                DrumManager drumManager = _dbContext.DrumManagers.Add(createDrumManager);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return drumManager;
            }
        }

        public void DeleteDrumManagerById(int id)
        {
            DrumManager drumManager = _dbContext.DrumManagers.FirstOrDefault(dm => dm.Id == id);
            if (drumManager == null) throw new NotFoundException<DrumManager>(id);

            _dbContext.DrumManagers.Remove(drumManager);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        public void DeleteDrumManagerByName(string drumManagerName)
        {
            DrumManager drumManager = _dbContext.DrumManagers.FirstOrDefault(dm => dm.Name == drumManagerName);
            if (drumManager == null) throw new NotFoundException<DrumManager>(drumManagerName);

            _dbContext.DrumManagers.Remove(drumManager);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }
        private bool DrumManagerExists(int id)
        {
            return _dbContext.DrumManagers.Count(dm => dm.Id == id) > 0;
        }
    }
}