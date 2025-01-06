using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerInfrastructure.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<WeddingPlannerDomain.Flower> ?Flower { get; set; }
        public DbSet<WeddingPlannerDomain.Entities.Food> ?Food { get; set; }
        public DbSet<User> ?Users { get; set; }
        public DbSet<Admin> ?Admins { get; set; }
        public DbSet<WeddingPlannerDomain.Entities.Location> ?Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Admin>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<WeddingPlannerDomain.Flower>()
               .Property(f => f.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<WeddingPlannerDomain.Entities.Food>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<WeddingPlannerDomain.Entities.Location>()
                .Property(l => l.Id)
                .ValueGeneratedOnAdd();

        }
       

        

    }
}
