using Microsoft.EntityFrameworkCore;
using Trades.Application.Interfaces;
using Trades.Application.Persistence;
using Trades.Application.Queries.Portfolio.GetCashBalance;
using Trades.Domain.Enums;

namespace Trades.Application.Services
{
    public class CashBalanceService : ICashBalanceService
    {
        private readonly ITradesDbContext _context;
        private readonly IReplenishmentBalanceReader _replenishmentBalanceReader;

        public CashBalanceService(ITradesDbContext context, IReplenishmentBalanceReader replenishmentBalanceReader)
        {
            _context = context;
            _replenishmentBalanceReader = replenishmentBalanceReader;
        }

        public async Task<GetCashBalanceResponse> GetBalanceAsync(string currency, CancellationToken cancellationToken)
        {
            var replenishments = await _replenishmentBalanceReader.GetAmountByCurrencyAsync(currency, cancellationToken);

            var buyAmount = await _context.Trades
                .AsNoTracking()
                .Where(t =>
                    t.Currency == currency &&
                    t.Type == TradeType.Buy)
                .SumAsync(
                    t => (decimal?)(t.Quantity * t.Price),
                    cancellationToken) ?? 0;

            var sellAmount = await _context.Trades
                .AsNoTracking()
                .Where(t =>
                    t.Currency == currency &&
                    t.Type == TradeType.Sell)
                .SumAsync(
                    t => (decimal?)(t.Quantity * t.Price),
                    cancellationToken) ?? 0;

            return new GetCashBalanceResponse
            {
                Currency = currency,
                Balance = replenishments - buyAmount + sellAmount
            };
        }

        public async Task<List<GetCashBalanceResponse>> GetBalancesAsync(CancellationToken cancellationToken)
        {
            var replenishments = await _replenishmentBalanceReader.GetAmountsByCurrencyAsync(cancellationToken);

            var trades = await _context.Trades
                .AsNoTracking()
                .GroupBy(g => g.Currency)
                .Select(g => new
                {
                    Currency = g.Key,
                    BuyAmount = g.Where(x => x.Type == TradeType.Buy).Sum(s => s.Quantity * s.Price),
                    SellAmount = g.Where(x => x.Type == TradeType.Sell).Sum(s => s.Quantity * s.Price)
                })
                .ToListAsync(cancellationToken);

            var balance = replenishments.Select(replenishment =>
            {
                var trade = trades.FirstOrDefault(item => item.Currency == replenishment.Key);

                return new GetCashBalanceResponse
                {
                    Currency = replenishment.Key,
                    Balance = replenishment.Value - (trade?.BuyAmount ?? 0) + (trade?.SellAmount ?? 0)
                };
            }).ToList();

            return balance;
        }
    }
}
