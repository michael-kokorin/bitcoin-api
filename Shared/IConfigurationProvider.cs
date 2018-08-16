namespace BitcoinApi.Shared
{
    public interface IConfigurationProvider
    {
        string GetValue(string key);
    }
}