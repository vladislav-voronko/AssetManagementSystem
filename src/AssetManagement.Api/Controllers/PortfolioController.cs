using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trades.Application.Queries.Portfolio.GetCashBalance;
using Trades.Application.Queries.Portfolio.GetPortfolioSummary;
using Trades.Application.Queries.Portfolio.GetPositions;

namespace AssetManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly GetCashBalanceHandler _cashBalanceHandler;
        private readonly GetPositionsHandler _positionsHandler;
        private readonly GetPortfolioSummaryHandler _portfolioSummaryHandler;

        public PortfolioController(GetCashBalanceHandler getCashBalanceHandler,
            GetPositionsHandler getPositionsHandler,
            GetPortfolioSummaryHandler getPortfolioSummaryHandler)
        {
            _cashBalanceHandler = getCashBalanceHandler;
            _positionsHandler = getPositionsHandler;
            _portfolioSummaryHandler = getPortfolioSummaryHandler;
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

        [HttpGet("positionsBalance")]
        [ProducesResponseType(typeof(List<GetPositionsResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPositions(CancellationToken cancellationToken)
        {
            var positionsBalance = await _positionsHandler.Handle(new GetPositionsQuery(), cancellationToken);
            return positionsBalance is null
                ? NotFound()
                : Ok(positionsBalance);
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(List<GetPortfolioSummaryResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPortfolioSummary(CancellationToken cancellationToken)
        {
            var portfolioSummary = await _portfolioSummaryHandler.Handle(new GetPortfolioSummaryQuery(), cancellationToken);
            return portfolioSummary is null
                ? NotFound()
                : Ok(portfolioSummary);
        }
    }
}
