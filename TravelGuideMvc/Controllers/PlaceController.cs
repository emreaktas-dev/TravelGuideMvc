using Microsoft.AspNetCore.Mvc;
using TravelGuideMvc.Models.Enums;
using TravelGuideMvc.Services.Interfaces;
using TravelGuideMvc.ViewModels;

namespace TravelGuideMvc.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly IServicePointService _servicePointService;

        public PlaceController(IPlaceService placeService, IServicePointService servicePointService)
        {
            _placeService = placeService;
            _servicePointService = servicePointService;
        }

        [HttpGet("/Place/{slug}")]
        public async Task<IActionResult> Detail(string slug, int? radius)
        {
            var place = await _placeService.GetBySlugAsync(slug);
            if (place == null)
                return NotFound();

            // Allow-list: güvenli ve kontrollü radius seçenekleri
            var allowed = new[] { 2000, 5000, 10000, 50000 };
            var selectedRadius = radius.HasValue && allowed.Contains(radius.Value) ? radius.Value : 5000;

            var vm = new PlaceDetailVm
            {
                Place = place,
                RadiusMeters = selectedRadius,
                NearbyHotels = await _servicePointService.GetNearbyAsync(place.Latitude, place.Longitude, selectedRadius, ServicePointType.Hotel, 6),
                NearbyRestaurants = await _servicePointService.GetNearbyAsync(place.Latitude, place.Longitude, selectedRadius, ServicePointType.Restaurant, 6),
                NearbyHospitals = await _servicePointService.GetNearbyAsync(place.Latitude, place.Longitude, selectedRadius, ServicePointType.Hospital, 6),
                NearbyATMs = await _servicePointService.GetNearbyAsync(place.Latitude, place.Longitude, selectedRadius, ServicePointType.ATM, 6)
            };

            return View(vm);
        }

    }
}
