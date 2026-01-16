using TravelGuideMvc.Models;
using TravelGuideMvc.Models.Enums;

namespace TravelGuideMvc.Services.Interfaces
{
    public interface IServicePointService
    {
        Task<List<ServicePoint>> GetNearbyAsync(
            double lat,
            double lng,
            int radiusMeters,
            ServicePointType? type = null,
            int take = 10);
    }
}
