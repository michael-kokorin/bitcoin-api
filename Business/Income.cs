using System;

namespace BitcoinApi.Business
{
    public sealed class Income
    {
        public DateTime Date { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int Confirmations { get; set; }
    }
}