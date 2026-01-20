using TravelGuideMvc.Models;

namespace TravelGuideMvc.Services.Interfaces
{
    public interface IEmergencyService
    {
        Task<List<EmergencyContact>> GetAllAsync();
    }
}
