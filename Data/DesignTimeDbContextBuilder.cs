using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BitcoinApi.Data
{
    internal sealed class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<BitcoinApiDbContext>
    {
        public BitcoinApiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BitcoinApiDbContext>();

            builder.UseSqlServer("Data Source=(local)\\MSS2017;Initial Catalog=BitcoinApi;Persist Security Info=True;User Id=sa;Password=495867;");

            return new BitcoinApiDbContext(builder.Options);
        }
    }
}