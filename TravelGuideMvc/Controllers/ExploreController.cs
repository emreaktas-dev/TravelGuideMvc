using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Data;
using TravelGuideMvc.ViewModels;

namespace TravelGuideMvc.Controllers
{
    public class ExploreController : Controller
    {
        private readonly AppDbContext _db;

        public ExploreController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("/Explore")]
        public async Task<IActionResult> Index(Guid? cityId, Guid? categoryId, string? search)
        {
            var vm = new ExploreVm
            {
                CityId = cityId,
                CategoryId = categoryId,
                Search = search,
                Cities = await _db.Cities.AsNoTracking().OrderBy(x => x.Name).ToListAsync(),
                Categories = await _db.Categories.AsNoTracking().OrderBy(x => x.Name).ToListAsync()
            };

            var q = _db.Places
                .AsNoTracking()
                .Include(x => x.City)
                .Include(x => x.Category)
                .Include(x => x.Images)
                .AsQueryable();

            if (cityId.HasValue)
                q = q.Where(x => x.CityId == cityId.Value);

            if (categoryId.HasValue)
                q = q.Where(x => x.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                q = q.Where(x => x.Name.Contains(search) || (x.ShortDescription != null && x.ShortDescription.Contains(search)));
            }

            vm.Places = await q.OrderBy(x => x.Name).ToListAsync();

            return View(vm);
        }
    }
}
