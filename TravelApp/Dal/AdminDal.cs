using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.Dal
{
    public class AdminDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SignUp>().ToTable("Admins");
        }

        public DbSet<SignUp> Admins { get; set; }
    }
}