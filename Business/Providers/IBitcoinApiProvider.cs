using BitcoinApi.Business.Models;
using BitcoinApi.Shared;

using JetBrains.Annotations;

namespace BitcoinApi.Business.Providers
{
    public interface IBitcoinApiProvider
    {
        [NotNull]
        [ItemNotNull]
        Wallet[] GetWallets();

        [NotNull]
        OperationResult<string> SendBtc([NotNull] string address, decimal amount, [CanBeNull] string comment = null);

        [NotNull]
        ListTransactionsResult ListTransactions([CanBeNull] string sinceBlockHash);
    }
}