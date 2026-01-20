using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.Extensions.Caching.Memory;
using TravelGuideMvc.Models.Dtos;
using TravelGuideMvc.Services.Interfaces;

namespace TravelGuideMvc.Services.Implementations
{
    public class NbkrCurrencyService : ICurrencyService
    {
        private const string CacheKey = "NBKR_RATES_TO_KGS";
        private const string NbkrDailyUrl = "https://www.nbkr.kg/XML/daily.xml";

        private readonly HttpClient _http;
        private readonly IMemoryCache _cache;

        public NbkrCurrencyService(HttpClient http, IMemoryCache cache)
        {
            _http = http;
            _cache = cache;
        }

        public async Task<Dictionary<string, decimal>> GetRatesToKgsAsync()
        {
            return await _cache.GetOrCreateAsync(CacheKey, async entry =>
            {
                // NBKR günlük güncel; gereksiz çağrıyı kesmek için cache
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

                var xml = await _http.GetStringAsync(NbkrDailyUrl);
                var doc = XDocument.Parse(xml);

                var rates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
                {
                    ["KGS"] = 1m
                };

                foreach (var c in doc.Descendants("Currency"))
                {
                    var code =
                        (string?)c.Element("ISOCode") ??
                        (string?)c.Element("CharCode") ??
                        (string?)c.Element("Code") ??
                        (string?)c.Attribute("ISOCode") ??
                        (string?)c.Attribute("CharCode") ??
                        (string?)c.Attribute("Code");

                    if (string.IsNullOrWhiteSpace(code))
                        continue;

                    var nominalStr = (string?)c.Element("Nominal") ?? "1";

                    var valueStr =
                        (string?)c.Element("Value") ??
                        (string?)c.Element("Rate");

                    if (string.IsNullOrWhiteSpace(valueStr))
                        continue;

                    if (!decimal.TryParse(nominalStr.Replace(',', '.'),
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var nominal))
                        nominal = 1m;

                    if (!decimal.TryParse(valueStr.Replace(',', '.'),
                            NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                        continue;

                    if (nominal <= 0) nominal = 1m;

                    // 1 UNIT = (value / nominal) KGS
                    var perUnit = value / nominal;

                    rates[code.Trim().ToUpperInvariant()] = perUnit;
                }

                // TRY NBKR'de yoksa: USD üzerinden çapraz kur ile TRY->KGS üret
                // NBKR: 1 USD = X KGS
                // Frankfurter: 1 USD = Y TRY
                // => 1 TRY = (X / Y) KGS
                if (!rates.ContainsKey("TRY") && rates.TryGetValue("USD", out var usdToKgs))
                {
                    var usdToTry = await GetUsdToTryFromFrankfurterAsync(); // 1 USD = ? TRY
                    if (usdToTry.HasValue && usdToTry.Value > 0)
                    {
                        rates["TRY"] = usdToKgs / usdToTry.Value;
                    }
                }

                return rates;
            }) ?? new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase) { ["KGS"] = 1m };
        }

        private async Task<decimal?> GetUsdToTryFromFrankfurterAsync()
        {
            // 1 USD -> ? TRY
            var url = "https://api.frankfurter.app/latest?from=USD&to=TRY";

            var json = await _http.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<FrankfurterLatestResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data?.Rates == null) return null;

            return data.Rates.TryGetValue("TRY", out var usdToTry) && usdToTry > 0
                ? usdToTry
                : null;
        }
    }
}
