using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business.RequestMethodFormatter
{
    internal sealed class ListSinceBlockFormatter: IRequestMethodFormatter
    {
        private readonly Regex _validator = new Regex("^[0-9a-z]+$");

        public string Method { get; } = "listsinceblock";

        public JArray Validate(Dictionary<string, string> parameters)
        {
            if(!parameters.TryGetValue("blockhash", out var blockHash))
            {
                throw new InvalidOperationException("Blockhash parameter is not provided");
            }

            if(!_validator.IsMatch(blockHash))
            {
                throw new InvalidOperationException("Blockhash parameter has invalid format");
            }

            var result = new JArray {blockHash};
            return result;
        }
    }
}