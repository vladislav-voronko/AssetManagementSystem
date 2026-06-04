using Trades.Application.Queries.Portfolio.GetCashBalance;
using Trades.Application.Queries.Portfolio.GetPositions;

namespace Trades.Application.Queries.Portfolio.GetPortfolioSummary
{
    public sealed class GetPortfolioSummaryResponse
    {
        public List<GetCashBalanceResponse> CashBalances { get; set; } = [];
        public List<GetPositionsResponse> Positions { get; set; } = [];
    }
}