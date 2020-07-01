using Autofac;
using Autofac.Integration.WebApi;
using EvalDrum.API.Controllers;
using EvalDrum.API.Services;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Http;

namespace EvalDrum.API
{
    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app, HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<EvalDrumContext>().InstancePerLifetimeScope();

            builder.RegisterType<DrumsService>().InstancePerLifetimeScope();
            builder.RegisterType<SitesService>().InstancePerLifetimeScope();
            builder.RegisterType<DrumManagersService>().InstancePerLifetimeScope();
            builder.RegisterType<StatusService>().InstancePerLifetimeScope();

            builder.RegisterApiControllers(typeof(DrumsController).Assembly);
            builder.RegisterApiControllers(typeof(SitesController).Assembly);
            builder.RegisterApiControllers(typeof(DrumManagersController).Assembly);
            builder.RegisterApiControllers(typeof(StatusController).Assembly);

            var CommonSharedAssembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "Common.Shared");
            builder.RegisterInstance(new ResourceManager("SharedServices.Resources.Resource", CommonSharedAssembly)).SingleInstance();
            builder.RegisterInstance(app.GetDataProtectionProvider()).SingleInstance();

            Container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);

            app.UseAutofacMiddleware(Container);
            app.UseAutofacWebApi(configuration);
        }
    }
}