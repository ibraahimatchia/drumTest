using DrumFunctionApp.Repositories.Interface;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrumFunctionApp.Services
{
    class DrumService
    {
        private TraceWriter _log;
        private readonly IDrumRepo _drumRepo;

        public DrumService(IDrumRepo drumRepo, TraceWriter log)
        {
            this._log = log;
            this._drumRepo = drumRepo;
        }

        public void Run()
        {
            try
            {
                _log.Info($"Timer Trigger DeleteDrumYearly function start at {DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToShortTimeString()}");
                _drumRepo.RemoveDrum();
                _log.Info($"DeleteDrumYearly function executed successfully at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                _log.Error($"DeleteDrumYearly function Failed at {DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToShortTimeString()} with Error message : {ex.Message}");
                throw;
            }
        }


    }
}
