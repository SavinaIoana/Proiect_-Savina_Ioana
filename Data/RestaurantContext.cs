using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect__Savina_Ioana.Models;

namespace Proiect__Savina_Ioana.Data
{
    public class RestaurantContext:DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) :
base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<OwnedFood> OwnedFood { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<Owners>().ToTable("Owners");
            modelBuilder.Entity<OwnedFood>().ToTable("OwnedFoods");
            modelBuilder.Entity<OwnedFood>()
            .HasKey(c => new { c.FoodID, c.OwnerID });
          
        }
    }
}
