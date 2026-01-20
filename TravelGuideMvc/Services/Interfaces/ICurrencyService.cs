namespace TravelGuideMvc.Services.Interfaces
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Returns rates as: 1 UNIT = X KGS (e.g., 1 USD = 87.45 KGS)
        /// Must include "KGS" => 1
        /// </summary>
        Task<Dictionary<string, decimal>> GetRatesToKgsAsync();
    }
}
