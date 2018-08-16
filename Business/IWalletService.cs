using BitcoinApi.Business.Models;
using BitcoinApi.Shared;

using JetBrains.Annotations;

namespace BitcoinApi.Business
{
    public interface IWalletService
    {
        [NotNull]
        OperationResult<string> SendBtc([NotNull] string address, decimal amount);

        [NotNull]
        [ItemNotNull]
        Income[] GetLastTransactions(int timestamp);
    }
}