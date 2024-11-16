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
        public DbSet<Flower> ?Flower { get; set; }
        public DbSet<Food> ?Food { get; set; }
        public DbSet<User> ?Users { get; set; }
        public DbSet<Admin> ?Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Admin>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Flower>()
               .Property(f => f.Id)
               .ValueGeneratedOnAdd();
        }
       

        

    }
}
