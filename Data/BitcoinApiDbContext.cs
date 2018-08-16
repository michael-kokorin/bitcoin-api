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

        protected override void OnConfiguring([NotNull] DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configurationProvider.GetValue("connectionstring:sqlserver"));
        }
    }
}