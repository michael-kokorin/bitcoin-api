using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business.RequestMethodFormatter
{
    internal sealed class SendToAddressFormatter: IRequestMethodFormatter
    {
        private readonly Regex _addressValidator = new Regex("^[0-9a-zA-Z]+$");

        public string Method { get; } = "sendtoaddress";

        public JArray Validate(Dictionary<string, string> parameters)
        {
            if(!parameters.TryGetValue("address", out var address))
            {
                throw new InvalidOperationException("Address parameter is not specified for sendtoaddress operation");
            }

            if(_addressValidator.IsMatch(address))
            {
                throw new InvalidOperationException("Address parameter is has invalid format");
            }

            if(!parameters.TryGetValue("amount", out var amountText))
            {
                throw new InvalidOperationException("Amount parameter is not specified for sendtoaddress operation");
            }

            if(!decimal.TryParse(amountText, out var amount))
            {
                throw new InvalidOperationException("Amount parameter is has invalid format");
            }

            var result = new JArray {address, amount};
            if(parameters.TryGetValue("comment", out var comment))
            {
                result.Add(comment);
            }

            return result;
        }
    }
}