namespace BitcoinApi.Business.Models
{
    public sealed class Income
    {
        public string Date { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int Confirmations { get; set; }

        public int Timestamp { get; set; }
    }
}