using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Interfaces;
using Trades.Application.Queries.Portfolio.GetCashBalance;
using Trades.Domain.Enums;

namespace Trades.Application.Services
{
    public class CashBalanceService : ICashBalanceService
    {
        private readonly BusinessDbContext _context;

        public CashBalanceService(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<GetCashBalanceResponse> GetBalanceAsync(string currency, CancellationToken cancellationToken)
        {
            var replenishments = await _context.Replenishments
                .AsNoTracking()
                .Where(r => r.Currency == currency)
                .SumAsync(r => (decimal?)r.Amount, cancellationToken) ?? 0;

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
            var replenishments = await _context.Replenishments
                .AsNoTracking()
                .GroupBy(r => r.Currency)
                .Select(r => new
                {
                    Currency = r.Key,
                    Amount = r.Sum(x => x.Amount)
                })
                .ToListAsync(cancellationToken);

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

            var balance = replenishments
                .Select(r =>
                {
                    var trade = trades.FirstOrDefault(t => t.Currency == r.Currency);

                    return new GetCashBalanceResponse
                    {
                        Currency = r.Currency,
                        Balance = r.Amount - (trade?.BuyAmount ?? 0) + (trade?.SellAmount ?? 0)
                    };
                }).ToList();

            return balance;
        }
    }
}
