using Autofac;

using BitcoinApi.Business.RequestMethodFormatter;

namespace BitcoinApi.Business
{
    public sealed class BusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WalletInfoUpdateJob>().As<IWalletInfoUpdateJob>().InstancePerDependency();
            builder.RegisterType<BitcoinApiProvider>().As<IBitcoinApiProvider>().InstancePerDependency();
            builder.RegisterType<RequestExecutor>().As<IRequestExecutor>().SingleInstance();
            builder.RegisterType<SendToAddressFormatter>().As<IRequestMethodFormatter>().SingleInstance();
            builder.RegisterType<ListReceivedByAddressFormatter>().As<IRequestMethodFormatter>().SingleInstance();
            builder.RegisterType<ListSinceBlockFormatter>().As<IRequestMethodFormatter>().SingleInstance();
        }
    }
}