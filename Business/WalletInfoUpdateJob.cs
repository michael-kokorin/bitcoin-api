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
        }
    }
}