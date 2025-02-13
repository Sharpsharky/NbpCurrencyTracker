using NbpCurrencyTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NbpCurrencyTracker.Services
{
    public class NbpApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://api.nbp.pl/api/exchangerates/tables/A/";

        public NbpApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<List<TableA>> GetCurrentRatesAsync()
        {
            return await FetchRatesAsync($"{BaseUrl}?format=json");
        }

        public async Task<List<TableA>> GetArchiveRatesAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var endDate = new DateTime(year, month, daysInMonth);

            var url = $"{BaseUrl}{startDate:yyyy-MM-dd}/{endDate:yyyy-MM-dd}/?format=json";
            return await FetchRatesAsync(url);
        }

        private async Task<List<TableA>> FetchRatesAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TableA>>(json);
                }
                else
                    throw new Exception($"Error fetching data: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception occurred while fetching rates: {ex.Message}");
            }
        }
    }
}
