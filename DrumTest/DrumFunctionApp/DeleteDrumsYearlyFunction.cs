using System;
using Autofac;
using DrumFunctionApp.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace DrumFunctionApp
{
    public static class DeleteDrumsYearlyFunction
    {
        [FunctionName("DeleteDrumsYearlyFunction")]
        public static void Run([TimerTrigger("*/3 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            IContainer container = Startup.SetIoContainer(log);
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                scope.Resolve<DrumService>().Run();
            }
        }
    }
}
