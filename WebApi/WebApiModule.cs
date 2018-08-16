using Autofac;

using BitcoinApi.Shared;
using BitcoinApi.WebApi.Providers;

namespace BitcoinApi.WebApi
{
    public sealed class WebApiModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
        }
    }
}