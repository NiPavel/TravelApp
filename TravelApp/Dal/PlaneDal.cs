using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.Dal
{
    public class PlaneDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plane>().ToTable("Planes");
        }

        public DbSet<Plane> Planes { get; set; }
    }
}