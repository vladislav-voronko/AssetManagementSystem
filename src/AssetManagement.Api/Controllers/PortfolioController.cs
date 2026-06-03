using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trades.Application.Queries.Portfolio.GetCashBalance;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly GetCashBalanceHandler _cashBalanceHandler;

        public PortfolioController(GetCashBalanceHandler getCashBalanceHandler)
        {
            _cashBalanceHandler = getCashBalanceHandler;
        }

        [HttpGet("cashBalance")]
        [ProducesResponseType(typeof(List<GetCashBalanceResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCashBalance(CancellationToken cancellationToken)
        {
            var cashBalance = await _cashBalanceHandler.Handle(new GetCashBalanceQuery(), cancellationToken);
            return cashBalance is null
                ? NotFound()
                : Ok(cashBalance);
        }
    }
}
