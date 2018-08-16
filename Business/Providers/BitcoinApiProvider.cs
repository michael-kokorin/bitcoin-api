using System;
using System.Collections.Generic;
using System.Globalization;

using BitcoinApi.Business.Models;
using BitcoinApi.Shared;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitcoinApi.Business.Providers
{
    internal sealed class BitcoinApiProvider: IBitcoinApiProvider
    {
        private readonly IRequestExecutor _requestExecutor;

        public BitcoinApiProvider(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor;
        }

        public Wallet[] GetWallets()
        {
            var resultText = _requestExecutor.Execute("listreceivedbyaddress", new Dictionary<string, string>());
            var result = JsonConvert.DeserializeObject<Wallet[]>(resultText);
            return result;
        }

        public OperationResult<string> SendBtc(string address, decimal amount, string comment = null)
        {
            var parameters = new Dictionary<string, string> {{"address", address}, {"amount", amount.ToString(CultureInfo.InvariantCulture)}};
            if(!string.IsNullOrWhiteSpace(comment))
            {
                parameters.Add("comment", comment);
            }

            try
            {
                var result = _requestExecutor.Execute("listreceivedbyaddress", parameters);
                return new OperationResult<string>
                {
                    Success = true,
                    Data = result
                };
            }
            catch(InvalidOperationException e)
            {
                return new OperationResult<string> {Success = false, Message = e.Message};
            }
        }

        public ListTransactionsResult ListTransactions(string sinceBlockHash)
        {
            var parameters = new Dictionary<string, string>();
            if(!string.IsNullOrWhiteSpace(sinceBlockHash))
            {
                parameters.Add("blockhash", sinceBlockHash);
            }

            var resultText =
                _requestExecutor.Execute("listsinceblock", parameters);
            var resultValue = JObject.Parse(resultText);
            var result = JsonConvert.DeserializeObject<ListTransactionsResult>(resultValue["result"].ToString());
            return result;
        }
    }
}