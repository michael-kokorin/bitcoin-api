using Newtonsoft.Json;

namespace BitcoinApi.Business.Models
{
    public sealed class Wallet
    {
        [JsonProperty(PropertyName = "account")]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}