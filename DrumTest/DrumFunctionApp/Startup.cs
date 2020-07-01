using Autofac;
using DrumFunctionApp.Repositories;
using DrumFunctionApp.Repositories.Interface;
using DrumFunctionApp.Services;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrumFunctionApp
{
    public static class Startup
    {
        public static IContainer SetIoContainer(TraceWriter traceWriter)
        {
            var builder = new ContainerBuilder();
            builder.Register(o => traceWriter).As<TraceWriter>();

            builder.RegisterType<EvalDrumContext>().As<EvalDrumContext, DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<DrumRepo>().As<IDrumRepo>();
            builder.RegisterType<DrumService>();

            return builder.Build();
        }
    }
}
