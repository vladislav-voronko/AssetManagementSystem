using Trades.Application.Interfaces;

namespace Trades.Application.Queries.Portfolio.GetCashBalance
{
    public class GetCashBalanceHandler
    {
        private readonly ICashBalanceService _cashBalanceService;

        public GetCashBalanceHandler(ICashBalanceService cashBalanceService)
        {
            _cashBalanceService = cashBalanceService;
        }

        public async Task<List<GetCashBalanceResponse>> Handle(GetCashBalanceQuery query, CancellationToken cancellationToken)
        {
            return await _cashBalanceService.GetBalancesAsync(cancellationToken);
        }
    }
}
