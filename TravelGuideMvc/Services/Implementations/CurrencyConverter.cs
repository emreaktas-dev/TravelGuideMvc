namespace TravelGuideMvc.Services.Implementations
{
    public static class CurrencyConverter
    {
        /// <summary>
        /// ratesToKgs: 1 UNIT = X KGS
        /// </summary>
        public static decimal Convert(decimal amount, string from, string to, Dictionary<string, decimal> ratesToKgs)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (string.IsNullOrWhiteSpace(from)) throw new ArgumentException("from is required");
            if (string.IsNullOrWhiteSpace(to)) throw new ArgumentException("to is required");

            from = from.Trim().ToUpperInvariant();
            to = to.Trim().ToUpperInvariant();

            if (!ratesToKgs.TryGetValue(from, out var fromToKgs))
                throw new InvalidOperationException($"Rate not found for: {from}");

            if (!ratesToKgs.TryGetValue(to, out var toToKgs))
                throw new InvalidOperationException($"Rate not found for: {to}");

            var kgs = amount * fromToKgs;     // X -> KGS
            var result = kgs / toToKgs;       // KGS -> Y

            return result;
        }
    }
}
