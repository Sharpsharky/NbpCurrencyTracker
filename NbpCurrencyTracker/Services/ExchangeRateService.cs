using System.Collections.Generic;
using System.Threading.Tasks;
using NbpCurrencyTracker.Models;

namespace NbpCurrencyTracker.Services
{
    public class ExchangeRateService
    {
        private readonly NbpApiClient apiClient;

        public ExchangeRateService()
        {
            apiClient = new NbpApiClient();
        }

        public async Task<List<ExchangeRate>> GetCurrentRatesAsync()
        {
            var tables = await apiClient.GetCurrentRatesAsync();
            return ExtractRates(tables);
        }

        public async Task<List<ExchangeRate>> GetArchiveRatesAsync(int year, int month)
        {
            var tables = await apiClient.GetArchiveRatesAsync(year, month);
            return ExtractRates(tables);
        }

        private List<ExchangeRate> ExtractRates(List<TableA> tables)
        {
            var rates = new List<ExchangeRate>();

            foreach (var table in tables)
            {
                foreach (var rate in table.Rates)
                {
                    rates.Add(new ExchangeRate
                    {
                        Date = table.EffectiveDate,
                        Currency = rate.Currency,
                        Code = rate.Code,
                        Rate = rate.Mid
                    });
                }
            }

            return rates;
        }
    }
}
