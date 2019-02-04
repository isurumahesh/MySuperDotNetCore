using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MySuper.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySuper.Api.DatabaseContext
{
    public class MySuperContext : DbContext
    {
        public MySuperContext(DbContextOptions<MySuperContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //shadow properties
            //modelBuilder.Entity<Customer>().Property<DateTime>("CreatedDate");
            //modelBuilder.Entity<Customer>().Property<DateTime>("UpdatedDate");

            //define shadow properties for all entities
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedDate");
            //    modelBuilder.Entity(entityType.Name).Property<DateTime>("UpdatedDate");
            //}

            //seeding database
            // modelBuilder.Entity<Product>().HasData(new { Id = 1, Name = "Sun Light", CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                FirstName = "Isuru",
                LastName = "Mahesh",
            });

            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "Sun Light" }, new Product { Id = 2, Name = "Sun silk" });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = 1,
                OrderDate = new DateTime(2019, 01, 20),
                CustomerId = 1,
            });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Price = 20 },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Price = 30 });


        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges();
        //    var timestamp = DateTime.Now;
        //    foreach (var entry in ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        //    {
        //        entry.Property("LastModified").CurrentValue = timestamp;

        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Property("Created").CurrentValue = timestamp;
        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}
