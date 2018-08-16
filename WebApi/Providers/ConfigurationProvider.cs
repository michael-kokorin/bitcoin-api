using BitcoinApi.Shared;

namespace BitcoinApi.WebApi.Providers
{
    internal sealed class ConfigurationProvider: IConfigurationProvider
    {
        public string GetValue(string key)
        {
            return Startup.Configuration[key];
        }
    }
}