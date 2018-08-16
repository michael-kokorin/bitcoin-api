using System;
using System.Linq;

using BitcoinApi.Business;
using BitcoinApi.Business.Models;
using BitcoinApi.Shared;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace BitcoinApi.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public sealed class ValuesController: ControllerBase
    {
        private readonly IWalletService _walletService;

        public ValuesController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [NotNull]
        [HttpPost("sendbtc")]
        public ActionResult<OperationResult> SendBtc([NotNull] string address, decimal amount)
        {
            var result = _walletService.SendBtc(address, amount);
            return result;
        }

        [NotNull]
        [HttpGet("getlast")]
        public ActionResult<Income[]> GetLast(int? timestamp)
        {
            var transactions = _walletService.GetLastTransactions(timestamp);
            return transactions;
        }
    }
}