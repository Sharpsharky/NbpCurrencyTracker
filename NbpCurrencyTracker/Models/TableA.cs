using Newtonsoft.Json;
using System.Collections.Generic;

namespace NbpCurrencyTracker.Models
{
    public class TableA
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("no")]
        public string No { get; set; }

        [JsonProperty("effectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonProperty("rates")]
        public List<Rate> Rates { get; set; }
    }
}
