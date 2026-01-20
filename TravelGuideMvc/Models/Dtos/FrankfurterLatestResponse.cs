namespace TravelGuideMvc.Models.Dtos
{
    public class FrankfurterLatestResponse
    {
        public decimal Amount { get; set; }
        public string Base { get; set; } = "";
        public string Date { get; set; } = "";
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
