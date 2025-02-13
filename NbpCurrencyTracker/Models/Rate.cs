using Newtonsoft.Json;

namespace NbpCurrencyTracker.Models
{
    public class Rate
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("mid")]
        public decimal Mid { get; set; }
    }
}
