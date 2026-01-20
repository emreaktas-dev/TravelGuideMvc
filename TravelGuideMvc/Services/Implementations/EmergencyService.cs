using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Data;
using TravelGuideMvc.Models;
using TravelGuideMvc.Services.Interfaces;

namespace TravelGuideMvc.Services.Implementations
{
    public class EmergencyService : IEmergencyService
    {
        private readonly AppDbContext _db;

        public EmergencyService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<EmergencyContact>> GetAllAsync()
        {
            return await _db.EmergencyContacts
                .AsNoTracking()
                .OrderBy(x => x.Priority)
                .ThenBy(x => x.Title)
                .ToListAsync();
        }
    }
}
