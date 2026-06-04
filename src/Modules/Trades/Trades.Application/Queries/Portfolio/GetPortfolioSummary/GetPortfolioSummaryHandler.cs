using Trades.Application.Interfaces;

namespace Trades.Application.Queries.Portfolio.GetPortfolioSummary
{
    public class GetPortfolioSummaryHandler
    {
        private readonly ICashBalanceService _cashBalanceService;
        private readonly IPositionService _positionService;

        public GetPortfolioSummaryHandler(ICashBalanceService cashBalanceService, IPositionService positionService)
        {
            _cashBalanceService = cashBalanceService;
            _positionService = positionService;
        }

        public async Task<GetPortfolioSummaryResponse> Handle(GetPortfolioSummaryQuery query, CancellationToken cancellationToken)
        {
            var cashBalances = await _cashBalanceService.GetBalancesAsync(cancellationToken);
            var positions = await _positionService.GetPositionsAsync(cancellationToken);

            return new GetPortfolioSummaryResponse { CashBalances = cashBalances, Positions = positions };
        }
    }
}
