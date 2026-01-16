using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class PlaceDetailVm
    {
        public Place Place { get; set; } = null!;

        public int RadiusMeters { get; set; } = 50000; // 50 km (MVP)
        public List<ServicePoint> NearbyHotels { get; set; } = new();
        public List<ServicePoint> NearbyRestaurants { get; set; } = new();
        public List<ServicePoint> NearbyHospitals { get; set; } = new();
        public List<ServicePoint> NearbyATMs { get; set; } = new();
    }
}
