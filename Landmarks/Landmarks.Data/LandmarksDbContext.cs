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

        public DbSet<Comment> Comments { get; set; }

        public DbSet<SubComment> SubComments { get; set; }

        public DbSet<VisitedPlaces> VisitedPlaces { get; set; }    

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

            builder.Entity<Landmark>()
                .HasIndex(x => x.Name)
                .IsUnique();

            builder.Entity<VisitedPlaces>()
                .HasKey(x => new { x.LandmarkId, x.UserId});

            builder.Entity<VisitedPlaces>()
                .HasOne(x => x.User)
                .WithMany(x=> x.VisitedPlaces)
                .HasForeignKey(x => x.UserId);

            builder.Entity<VisitedPlaces>()
                .HasOne(x => x.Landmark)
                .WithMany(x => x.Visitors)
                .HasForeignKey(x => x.LandmarkId);

            base.OnModelCreating(builder);
        }
    }
}
