using BitcoinApi.Shared;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace BitcoinApi.Data
{
    internal sealed class BitcoinApiDbContext: DbContext, IDbContext
    {
        private readonly IConfigurationProvider _configurationProvider;

        public BitcoinApiDbContext(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public BitcoinApiDbContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnConfiguring([NotNull] DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configurationProvider.GetValue("connectionstring:sqlserver"));
            }
        }

        public DbSet<WalletEntity> Wallets { get; set; }

        public DbSet<TransactionEntity> Transactions { get; set; }
    }
}