using Newtonsoft.Json;

namespace BitcoinApi.Business
{
    /*
     * "account": "Test1",
      "address": "2N8q8fQhs8uiyNgSz997aii3LWeARy6f7uh",
      "category": "receive",
      "amount": 1.10000000,
      "label": "Test1",
      "vout": 1,
      "confirmations": 1,
      "blockhash": "00000000000000525cc7f3b9bc1dcd92550a63e87dec62b5dd7f1d5def52d299",
      "blockindex": 1197,
      "blocktime": 1534395701,
      "txid": "94ca5541d9293da08ae6864c458cca4bc374fecef179af64baf84228682ac581",
      "walletconflicts": [
      ],
      "time": 1534394214,
      "timereceived": 1534394214,
      "bip125-replaceable": "no"
     */
    public sealed class Transaction
    {
        [JsonProperty(PropertyName = "account")]
        public string Account { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty(PropertyName = "blockhash")]
        public string BlockHash { get; set; }

        [JsonProperty(PropertyName = "blockindex")]
        public int BlockIndex { get; set; }

        [JsonProperty(PropertyName = "blocktime")]
        public int BlockTime { get; set; }

        [JsonProperty(PropertyName = "time")]
        public int Time { get; set; }

        [JsonProperty(PropertyName = "timereceived")]
        public int TimeReceived { get; set; }
    }
}