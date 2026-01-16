using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class ExploreVm
    {
        public List<Place> Places { get; set; } = new();

        public List<City> Cities { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        public Guid? SelectedCityId { get; set; }
        public Guid? SelectedCategoryId { get; set; }
    }
}
