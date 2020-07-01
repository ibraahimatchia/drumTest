namespace EvalDrum.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<EvalDrumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EvalDrumContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Status.AddOrUpdate(st => st.Id,
                new Status() { Id = 1, Status_name = "OnSite", CreatedOn = DateTime.Parse(DateTime.Today.ToString()) }
                );

            context.Sites.AddOrUpdate(s => s.Id,
                new Site() { Id = 1, Name = "SERVAL_LOGISTIC_SITE_BORDEAUX", Street = null, Street2 = null, Street3 = null, PostalCode = "33100", City = "Bordeaux", Country = "France", Tel = null, Fax = null, EMail = null}
                );

            context.DrumManagers.AddOrUpdate(m => m.Id,
                new DrumManager() { Id = 1, Name = "Nexans France", ContactEmail = "france@nexans.com"}
                );

            context.Drums.AddOrUpdate(d => d.Id,
                new Drum() { Id = 1, DrumNumber = "IBGA173955", CreatedOn = DateTime.Parse(DateTime.Today.ToString()), Latitude = 50.51495, Longitude = 3.61610000000002, InPositionSince = DateTime.Parse(DateTime.Today.ToString()), Site_Id = 1, Status_Id = 1, DrumManager_Id = 1 }
                );


        }
    }
}
