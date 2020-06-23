using DAL.Models;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationDrum.Services
{
    public class DrumService
    {
        private readonly IDrumRepository _drumRepository;

        public DrumService(IDrumRepository drumRepository)
        {
            this._drumRepository = drumRepository;
        }

        public IEnumerable<Drum> GetDrums()
        {
            IEnumerable<Drum> drums = _drumRepository.GetDrums();
            return drums;
        }
    }

}