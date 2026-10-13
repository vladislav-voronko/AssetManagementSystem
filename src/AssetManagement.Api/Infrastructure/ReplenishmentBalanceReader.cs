using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Persistence;
using Trades.Application.Interfaces;

namespace AssetManagement.Api.Infrastructure
{
    public sealed class ReplenishmentBalanceReader : IReplenishmentBalanceReader
    {
        private readonly IReplenishmentsDbContext _context;

        public ReplenishmentBalanceReader(IReplenishmentsDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetAmountByCurrencyAsync(string currency, CancellationToken cancellationToken)
        {
            return await _context.Replenishments
                .Where(replenishment => replenishment.Currency == currency)
                .SumAsync(replenishment => (decimal?)replenishment.Amount, cancellationToken) ?? 0;
        }

        public async Task<IReadOnlyDictionary<string, decimal>> GetAmountsByCurrencyAsync(CancellationToken cancellationToken)
        {
            return await _context.Replenishments
                .GroupBy(replenishment => replenishment.Currency)
                .Select(group => new { Currency = group.Key, Amount = group.Sum(replenishment => replenishment.Amount) })
                .ToDictionaryAsync(item => item.Currency, item => item.Amount, cancellationToken);
        }
    }
}
