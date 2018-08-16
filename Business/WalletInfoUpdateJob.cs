using System;
using System.Linq;

using BitcoinApi.Business.Providers;
using BitcoinApi.Data;

namespace BitcoinApi.Business
{
    internal sealed class WalletInfoUpdateJob: IWalletInfoUpdateJob
    {
        private readonly IDbContext _dbContext;

        private readonly IBitcoinApiProvider _bitcoinApiProvider;

        public WalletInfoUpdateJob(IDbContext dbContext, IBitcoinApiProvider bitcoinApiProvider)
        {
            _dbContext = dbContext;
            _bitcoinApiProvider = bitcoinApiProvider;
        }

        public void DoJob()
        {
            UpdateTransactions();
            UpdateWallets();
        }

        private void UpdateWallets()
        {
            var entityWallets = _dbContext.Wallets.ToArray();
            var actualWallets = _bitcoinApiProvider.GetWallets().ToDictionary(_ => _.Address, _ => _);
            var newWallets = actualWallets.Values.Where(_ => entityWallets.All(entityWallet => entityWallet.Address != _.Address))
                .ToArray();
            foreach(var newWallet in newWallets)
            {
                _dbContext.Wallets.Add(new WalletEntity
                {
                    Account = newWallet.Account,
                    Address = newWallet.Address,
                    Balance = newWallet.Amount,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Removed = false
                });
            }

            foreach(var entityWallet in entityWallets)
            {
                if(!actualWallets.ContainsKey(entityWallet.Address))
                {
                    entityWallet.Removed = true;
                }
                else
                {
                    var actualWallet = actualWallets[entityWallet.Address];
                    entityWallet.Account = actualWallet.Account;
                    entityWallet.Balance = actualWallet.Amount;
                    entityWallet.Updated = DateTime.UtcNow;
                    entityWallet.Removed = false;
                }

                _dbContext.Wallets.Update(entityWallet);
            }

            _dbContext.SaveChanges();
        }

        private void UpdateTransactions()
        {
            var requiresUpdatingTransactions = _dbContext.Transactions.Where(_ => _.Category == "received")
                .Where(_ => _.Confirmations < 6)
                .ToDictionary(_ => _.TransactionId, _ => _);
            var lastReceivedTransation = _dbContext.Transactions.OrderByDescending(_ => _.Time).FirstOrDefault();
            var listTransactionsResult = _bitcoinApiProvider.ListTransactions(null);
            var newTransactions = listTransactionsResult.Transactions;
            if(lastReceivedTransation != null)
            {
                newTransactions = newTransactions.Where(_ => _.Time > lastReceivedTransation.Time).ToArray();
            }

            foreach(var transaction in newTransactions)
            {
                _dbContext.Transactions.Add(new TransactionEntity
                {
                    Account = transaction.Account,
                    Address = transaction.Address,
                    Amount = transaction.Amount,
                    BlockHash = transaction.BlockHash,
                    BlockIndex = transaction.BlockIndex,
                    BlockTime = transaction.BlockTime,
                    Category = transaction.Category,
                    Confirmations = transaction.Confirmations,
                    Label = transaction.Label,
                    Time = transaction.Time,
                    TimeReceived = transaction.TimeReceived,
                    TransactionId = transaction.TransactionId
                });
            }

            foreach(var updatedTransaction in listTransactionsResult.Transactions.Where(_ => requiresUpdatingTransactions.ContainsKey(_.TransactionId)))
            {
                var entityTransaction = requiresUpdatingTransactions[updatedTransaction.TransactionId];
                entityTransaction.Confirmations = updatedTransaction.Confirmations;
                _dbContext.Transactions.Update(entityTransaction);
            }

            _dbContext.SaveChanges();
        }
    }
}