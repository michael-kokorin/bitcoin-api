using System;
using System.Linq;

using BitcoinApi.Business.Models;
using BitcoinApi.Business.Providers;
using BitcoinApi.Data;
using BitcoinApi.Shared;

namespace BitcoinApi.Business
{
    internal sealed class WalletService: IWalletService
    {
        private readonly IBitcoinApiProvider _bitcoinApiProvider;

        private readonly IDbContext _dbContext;

        public WalletService(IBitcoinApiProvider bitcoinApiProvider, IDbContext dbContext)
        {
            _bitcoinApiProvider = bitcoinApiProvider;
            _dbContext = dbContext;
        }

        public OperationResult<string> SendBtc(string address, decimal amount)
        {
            if(amount <= 0)
            {
                return new OperationResult<string>
                {
                    Success = false,
                    Message = "Transfer amount can be less than zero or equal to zero"
                };
            }

            return _bitcoinApiProvider.SendBtc(address, amount);
        }

        public Income[] GetLastTransactions(int? timestamp)
        {
            var query = _dbContext.Transactions.Where(_ => _.Category == "receive");
            if(timestamp.HasValue)
            {
                query = query.Where(_ => timestamp < _.TimeReceived || _.Confirmations < 3);
            }

            var result = query
                .Select(_ => new Income
                {
                    Address = _.Address,
                    Amount = _.Amount,
                    Confirmations = _.Confirmations,
                    Timestamp = _.TimeReceived
                })
                .ToArray();
            foreach(var income in result)
            {
                income.Date = DateTime.UnixEpoch.AddSeconds(income.Timestamp).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return result;
        }
    }
}