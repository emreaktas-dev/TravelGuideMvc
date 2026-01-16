using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Data;
using TravelGuideMvc.Services.Interfaces;
using TravelGuideMvc.ViewModels;

namespace TravelGuideMvc.Controllers
{
    public class ExploreController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IPlaceService _placeService;

        public ExploreController(AppDbContext db, IPlaceService placeService)
        {
            _db = db;
            _placeService = placeService;
        }

        public async Task<IActionResult> Index(Guid? cityId, Guid? categoryId)
        {
            var vm = new ExploreVm
            {
                Places = await _placeService.GetAllAsync(cityId, categoryId),
                Cities = await _db.Cities.OrderBy(x => x.Name).ToListAsync(),
                Categories = await _db.Categories.OrderBy(x => x.Name).ToListAsync(),
                SelectedCityId = cityId,
                SelectedCategoryId = categoryId
            };

            return View(vm);
        }
    }
}
