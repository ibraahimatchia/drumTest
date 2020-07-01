using Common.Shared.ExceptionHandling;
using EvalDrum.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvalDrum.API.Services
{
    public class StatusService
    {
        private readonly EvalDrumContext _dbContext;

        public StatusService(EvalDrumContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Status> GetStatus()
        {
            var status = _dbContext.Status;
            return status;
        }

        public Status GetStatusById(int id)
        {
            var status = _dbContext.Status.SingleOrDefault(st => st.Id == id);

            return status;
        }

        public Status UpdateStatus(int statusId, Status statusDetail)
        {
            if (!StatusExists(statusId))
            {
                throw new BadRequestException($"Status with Id {statusId} doesn't exist.", BadRequestException.STATUS_DOESNT_EXISTS);
            }
            else
            {
                Status status = _dbContext.Status.FirstOrDefault(st => st.Id.Equals(statusId));

                status.Status_name = statusDetail.Status_name;

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return new Status
                {
                    Id = status.Id,
                    Status_name = status.Status_name,
                    CreatedOn = status.CreatedOn
                };
            }
        }

        public Status CreatStatus(Status createStatus)
        {
            if (_dbContext.Status.FirstOrDefault(st => st.Status_name == createStatus.Status_name) != null)
            {
                throw new BadRequestException($"Status with name {createStatus.Status_name} already exist.", BadRequestException.DUPLICATE_STATUS_NAME);
            }
            else
            {
                if(createStatus.CreatedOn == null) createStatus.CreatedOn = DateTime.UtcNow;

                Status status = _dbContext.Status.Add(createStatus);

                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.ToString());
                }
                return status;
            }
        }

        public void DeleteStatusById(int id)
        {
            Status status = _dbContext.Status.FirstOrDefault(st => st.Id == id);
            if (status == null) throw new NotFoundException<Status>(id);

            _dbContext.Status.Remove(status);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        public void DeleteStatusByName(string statusName)
        {
            Status status = _dbContext.Status.FirstOrDefault(st => st.Status_name == statusName);
            if (status == null) throw new NotFoundException<Status>(statusName);

            _dbContext.Status.Remove(status);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.ToString());
            }
        }

        private bool StatusExists(int id)
        {
            return _dbContext.Status.Count(st => st.Id == id) > 0;
        }
    }
}