using BitcoinApi.Business;
using BitcoinApi.Shared;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace BitcoinApi.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public sealed class ValuesController: ControllerBase
    {
        [NotNull]
        [HttpPost("sendbtc")]
        public ActionResult<OperationResult> SendBtc(string address, decimal amount)
        {
            return new OperationResult();
        }

        [NotNull]
        [HttpGet("getlast")]
        public ActionResult<Income[]> GetLast()
        {
            return new Income[0];
        }
    }
}