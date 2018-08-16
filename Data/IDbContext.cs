using Microsoft.EntityFrameworkCore;

namespace BitcoinApi.Data
{
    public interface IDbContext
    {
        DbSet<WalletEntity> Wallets { get; set; }

        DbSet<TransactionEntity> Transactions { get; set; }

        int SaveChanges();
    }
}