using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Data;
using TravelGuideMvc.Models;
using TravelGuideMvc.Services.Interfaces;

namespace TravelGuideMvc.Services.Implementations
{
    public class PlaceService : IPlaceService
    {
        private readonly AppDbContext _db;

        public PlaceService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Place?> GetBySlugAsync(string slug)
        {
            return await _db.Places
                .Include(x => x.City)
                .Include(x => x.Category)
                .Include(x => x.Images.OrderBy(i => i.Order))
                .FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<List<Place>> GetAllAsync(Guid? cityId, Guid? categoryId)
        {
            var query = _db.Places
                .Include(x => x.City)
                .Include(x => x.Category)
                .AsQueryable();

            if (cityId.HasValue)
                query = query.Where(x => x.CityId == cityId.Value);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            return await query
                .OrderBy(x => x.Name)
                .ToListAsync();


        }
    }
}
