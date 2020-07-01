using Autofac;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EvalDrum.API
{
    public partial class Startup
    {
        public void ConfigureDatabase(IAppBuilder app)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                new EvalDrumDbInitializer().InitializeDatabase(scope.Resolve<EvalDrumContext>());
            }
        }

        private class EvalDrumDbInitializer : IDatabaseInitializer<EvalDrumContext>
        {
            public void InitializeDatabase(EvalDrumContext context)
            {
                context.Database.CommandTimeout = 300;
                if (!context.Database.Exists())
                {
                    // TAL: while we do not have the InitCreate script as MigrationHistory script
                    // (already created in the Database before we implement the MigrationHistory),
                    // the Database.Create() cannot work.
                    // Use a .bacpac from the dev environment instead.
                    context.Database.Create();
                }
                else if (!context.Database.CompatibleWithModel(false))
                {
                    new MigrateDatabaseToLatestVersion<EvalDrumContext, EvalDrum.DAL.Migrations.Configuration>()
                        .InitializeDatabase(context);
                }

                context.Database.CommandTimeout = 60;
            }
        }
    }
}