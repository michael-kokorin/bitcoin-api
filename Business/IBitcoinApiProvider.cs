using BitcoinApi.Shared;

using JetBrains.Annotations;

namespace BitcoinApi.Business
{
    public interface IBitcoinApiProvider
    {
        [NotNull]
        [ItemNotNull]
        Wallet[] GetWallets();

        [NotNull]
        OperationResult<string> SendBtc(string address, decimal amount, string comment = null);

        [NotNull]
        ListTransactionsResult ListTransactions(string sinceBlockHash);
    }
}