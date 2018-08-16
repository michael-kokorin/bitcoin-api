using Autofac;

namespace BitcoinApi.Data
{
    public sealed class DataModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BitcoinApiDbContext>().As<IDbContext>().InstancePerDependency();
        }
    }
}