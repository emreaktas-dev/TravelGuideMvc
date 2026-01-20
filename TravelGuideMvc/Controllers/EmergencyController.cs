using Microsoft.AspNetCore.Mvc;
using TravelGuideMvc.Services.Interfaces;
using TravelGuideMvc.ViewModels;

namespace TravelGuideMvc.Controllers
{
    public class EmergencyController : Controller
    {
        private readonly IEmergencyService _emergencyService;

        public EmergencyController(IEmergencyService emergencyService)
        {
            _emergencyService = emergencyService;
        }

        [HttpGet("/Emergency")]
        public async Task<IActionResult> Index()
        {
            var vm = new EmergencyVm
            {
                Contacts = await _emergencyService.GetAllAsync()
            };

            return View(vm);
        }
    }
}
