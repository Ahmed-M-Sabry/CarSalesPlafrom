using CarSales.Domain.Entities;
using CarSales.Domain.Entities.CarDetails;
using CarSales.Domain.Entities.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        // Posts
        public DbSet<NewCarPost> NewCarPosts { get; set; }
        public DbSet<OldCarPost> OldCarPosts { get; set; }

        // Images
        public DbSet<NewCarImage> NewCarImages { get; set; }
        public DbSet<UsedCarImage> UsedCarImages { get; set; }

        // CarDetails
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }

    }
}
