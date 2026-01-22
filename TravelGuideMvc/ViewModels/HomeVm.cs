using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class HomeVm
    {
        // Ana sayfada göstereceğimiz öne çıkan mekanlar listesi
        public List<Place> FeaturedPlaces { get; set; } = new List<Place>();
    }
}