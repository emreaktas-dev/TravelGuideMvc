using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // .Include() için gerekli
using TravelGuideMvc.Data; // AppDbContext için gerekli (Data klasöründe olduðunu varsayýyorum)
using TravelGuideMvc.Models;
using TravelGuideMvc.ViewModels; // HomeVm için gerekli

namespace TravelGuideMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // Veritabaný servisi

        // Constructor'a AppDbContext ekledik
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Veritabanýndan rastgele veya ilk 6 mekaný çekiyoruz.
            // .Include ile Þehir ve Kategori bilgilerini de getiriyoruz ki kartlarda boþ çýkmasýn.
            var featuredPlaces = _context.Places
                                         .Include(x => x.City)
                                         .Include(x => x.Category)
                                         .OrderBy(x => x.Id) // Veya x.Name, isteðe baðlý sýralama
                                         .Take(6) // Ana sayfada çok yýðýlma olmasýn diye sýnýrla
                                         .ToList();

            // ViewModel'i oluþturup veriyi içine koyuyoruz
            var vm = new HomeVm
            {
                FeaturedPlaces = featuredPlaces
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}