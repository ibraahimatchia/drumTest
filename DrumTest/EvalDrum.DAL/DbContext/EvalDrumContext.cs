﻿using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

public class EvalDrumContext : IdentityDbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public EvalDrumContext() : base("name=EvalDrumContext")
    {
        this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
    }

    public virtual DbSet<EvalDrum.DAL.Models.Drum> Drums { get; set; }

    public virtual DbSet<EvalDrum.DAL.Models.DrumManager> DrumManagers { get; set; }

    public virtual DbSet<EvalDrum.DAL.Models.Site> Sites { get; set; }

    public virtual DbSet<EvalDrum.DAL.Models.Status> Status { get; set; }
}
