using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business.RequestMethodFormatter
{
    internal sealed class ListReceivedByAddressFormatter: IRequestMethodFormatter
    {
        public string Method { get; } = "listreceivedbyaddress";

        public JArray Validate(Dictionary<string, string> parameters)
        {
            var result = new JArray {0, true};
            return result;
        }
    }
}