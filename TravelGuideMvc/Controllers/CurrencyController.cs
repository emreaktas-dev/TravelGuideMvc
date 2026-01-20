using Microsoft.AspNetCore.Mvc;
using TravelGuideMvc.Services.Implementations;
using TravelGuideMvc.Services.Interfaces;
using TravelGuideMvc.ViewModels;

namespace TravelGuideMvc.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("/Currency")]
        public async Task<IActionResult> Index(decimal? amount, string? from, string? to)
        {
            var vm = new CurrencyVm
            {
                Amount = amount.HasValue && amount.Value > 0 ? amount.Value : 1m,
                From = string.IsNullOrWhiteSpace(from) ? "KGS" : from!,
                To = string.IsNullOrWhiteSpace(to) ? "USD" : to!
            };

            try
            {
                vm.RatesToKgs = await _currencyService.GetRatesToKgsAsync();

                // Supported listesini gerçekten gelen rate’lere göre filtrele
                vm.Supported = vm.Supported
                    .Where(x => vm.RatesToKgs.ContainsKey(x))
                    .ToList();

                // Eğer seçilen para birimi yoksa fallback
                if (!vm.Supported.Contains(vm.From.Trim().ToUpperInvariant())) vm.From = "KGS";
                if (!vm.Supported.Contains(vm.To.Trim().ToUpperInvariant())) vm.To = "USD";

                vm.Result = CurrencyConverter.Convert(vm.Amount, vm.From, vm.To, vm.RatesToKgs);
            }
            catch (Exception ex)
            {
                vm.Error = ex.Message;
            }

            return View(vm);
        }
    }
}
