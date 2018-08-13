using Landmarks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Data
{
    public class LandmarksDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Landmark> Landmarks { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Image> Images { get; set; }

        public LandmarksDbContext(DbContextOptions<LandmarksDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<Region>()
                .HasIndex(x => x.Name)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
