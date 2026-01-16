using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Models;

namespace TravelGuideMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities => Set<City>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Place> Places => Set<Place>();
        public DbSet<PlaceImage> PlaceImages => Set<PlaceImage>();
        public DbSet<ServicePoint> ServicePoints => Set<ServicePoint>();
        public DbSet<EmergencyContact> EmergencyContacts => Set<EmergencyContact>();
        public DbSet<CultureArticle> CultureArticles => Set<CultureArticle>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Slug'lar (çok işine yarayacak)
            modelBuilder.Entity<City>().HasIndex(x => x.Slug).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Slug).IsUnique();
            modelBuilder.Entity<Place>().HasIndex(x => x.Slug).IsUnique();

            modelBuilder.Entity<CultureArticle>()
                .HasIndex(x => new { x.Slug, x.Language })
                .IsUnique();

            // İlişkiler
            modelBuilder.Entity<PlaceImage>()
                .HasOne(pi => pi.Place)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
