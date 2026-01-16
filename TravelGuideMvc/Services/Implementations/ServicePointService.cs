using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Data;
using TravelGuideMvc.Models;
using TravelGuideMvc.Models.Enums;
using TravelGuideMvc.Services.Interfaces;

namespace TravelGuideMvc.Services.Implementations
{
    public class ServicePointService : IServicePointService
    {
        private readonly AppDbContext _db;

        public ServicePointService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ServicePoint>> GetNearbyAsync(
            double lat,
            double lng,
            int radiusMeters,
            ServicePointType? type = null,
            int take = 10)
        {
            // Not: EF Core içinde Haversine'i direkt SQL'e çevirmek zor.
            // MVP için: önce makul sayıda aday çekip sonra C# ile hesaplıyoruz.

            var query = _db.ServicePoints.AsNoTracking().AsQueryable();

            if (type.HasValue)
                query = query.Where(x => x.Type == type.Value);

            // Basit bounding box ile DB tarafında daraltma (performans için)
            // 1 derece enlem ~ 111km
            var radiusKm = radiusMeters / 1000.0;
            var latDelta = radiusKm / 111.0;
            var lngDelta = radiusKm / (111.0 * Math.Cos(Deg2Rad(lat)));

            var minLat = lat - latDelta;
            var maxLat = lat + latDelta;
            var minLng = lng - lngDelta;
            var maxLng = lng + lngDelta;

            var candidates = await query
                .Where(x => x.Latitude >= minLat && x.Latitude <= maxLat)
                .Where(x => x.Longitude >= minLng && x.Longitude <= maxLng)
                .ToListAsync();

            // Gerçek mesafe filtresi
            var result = candidates
                .Select(sp => new
                {
                    Sp = sp,
                    Dist = HaversineMeters(lat, lng, sp.Latitude, sp.Longitude)
                })
                .Where(x => x.Dist <= radiusMeters)
                .OrderBy(x => x.Dist)
                .Take(take)
                .Select(x => x.Sp)
                .ToList();

            return result;
        }

        private static double HaversineMeters(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371000; // meters
            var dLat = Deg2Rad(lat2 - lat1);
            var dLon = Deg2Rad(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double Deg2Rad(double deg) => deg * (Math.PI / 180.0);
    }
}
