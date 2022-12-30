using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.Dal
{
    public class OrderDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Orders");
        }

        public DbSet<Order> Orders { get; set; }
    }
}