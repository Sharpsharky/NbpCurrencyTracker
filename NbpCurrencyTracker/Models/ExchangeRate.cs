namespace NbpCurrencyTracker.Models
{
    public class ExchangeRate
    {
        public string Date { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
    }
}
