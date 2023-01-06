using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.Dal
{
    public class PaymentDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Payment>().ToTable("Payments");
        }

        public DbSet<Payment> Payments { get; set; }
    }
}