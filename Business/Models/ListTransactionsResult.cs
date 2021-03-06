﻿using Newtonsoft.Json;

namespace BitcoinApi.Business.Models
{
    public sealed class ListTransactionsResult
    {
        [JsonProperty(PropertyName = "lastblock")]
        public string BlockHash { get; set; }

        [JsonProperty(PropertyName = "transactions")]
        public Transaction[] Transactions { get; set; }
    }
}