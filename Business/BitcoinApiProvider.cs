﻿using System;
using System.Collections.Generic;
using System.Globalization;

using BitcoinApi.Shared;

using Newtonsoft.Json;

namespace BitcoinApi.Business
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
            var resultText =
                _requestExecutor.Execute("listsinceblock", new Dictionary<string, string> {{"blockhash", sinceBlockHash}});
            var result = JsonConvert.DeserializeObject<ListTransactionsResult>(resultText);
            return result;
        }
    }
}