using ASP.NET_MVC_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP.NET_MVC_Project
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } 
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("category");
            modelBuilder.Entity<Product>().ToTable("product");
        }
    }
}
