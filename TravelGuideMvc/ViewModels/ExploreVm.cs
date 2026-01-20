using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class ExploreVm
    {
        // Filtreler (querystring)
        public Guid? CityId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Search { get; set; }

        // Dropdown listeleri
        public List<City> Cities { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        // Ana liste
        public List<Place> Places { get; set; } = new();
    }
}
