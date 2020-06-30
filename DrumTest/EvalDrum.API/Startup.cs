using Autofac;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(EvalDrum.API.Startup))]

namespace EvalDrum.API
{
    public partial class Startup
    {
        private static IContainer Container { get; set; }
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureOAuth(app);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            config.Formatters[0] = new JsonMediaTypeFormatter
            {
                SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
                }
            };

            ConfigureAutofac(app, config);
            //ConfigureDatabase(app);
            app.UseWebApi(config);
        }

    
    }
}
