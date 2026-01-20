using TravelGuideMvc.Models;

namespace TravelGuideMvc.ViewModels
{
    public class EmergencyVm
    {
        public List<EmergencyContact> Contacts { get; set; } = new();
    }
}
