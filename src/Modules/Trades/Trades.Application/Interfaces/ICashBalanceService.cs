using Trades.Application.Queries.Portfolio.GetCashBalance;

namespace Trades.Application.Interfaces
{
    public interface ICashBalanceService
    {
        public Task<GetCashBalanceResponse> GetBalanceAsync(string currency, CancellationToken cancellationToken);
        public Task<List<GetCashBalanceResponse>> GetBalancesAsync(CancellationToken cancellationToken);
    }
}
