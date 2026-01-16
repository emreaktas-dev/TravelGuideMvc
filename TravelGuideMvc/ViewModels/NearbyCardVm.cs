using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class NearbyCardVm
    {
        public string Title { get; set; } = "";
        public string Icon { get; set; } = "";
        public bool ShowPhone { get; set; }

        public List<ServicePoint> Items { get; set; } = new();
    }
}
