using TravelGuideMvc.Models;

namespace TravelGuideMvc.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<List<Place>> GetAllAsync(Guid? cityId, Guid? categoryId);
        Task<Place?> GetBySlugAsync(string slug);
    }
}
