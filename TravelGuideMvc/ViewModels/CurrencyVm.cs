namespace TravelGuideMvc.ViewModels
{
    public class CurrencyVm
    {
        public Dictionary<string, decimal> RatesToKgs { get; set; } = new();

        public decimal Amount { get; set; } = 1m;
        public string From { get; set; } = "KGS";
        public string To { get; set; } = "USD";

        public decimal? Result { get; set; }
        public string? Error { get; set; }

        // UI’de göstereceğimiz para birimleri
        public List<string> Supported { get; set; } = new() { "KGS", "USD", "EUR", "TRY", "RUB" };
    }
}
