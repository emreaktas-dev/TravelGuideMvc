using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Models;

namespace TravelGuideMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Şimdilik boş - birazdan modelleri ekleyince buraya DbSet yazacağız.
        // public DbSet<Place> Places => Set<Place>();
    }
}
