using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Interfaces;
using Trades.Application.Queries.Portfolio.GetPositions;
using Trades.Domain.Enums;

namespace Trades.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly BusinessDbContext _context;

        public PositionService(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetPositionAsync(Guid assetId, CancellationToken cancellationToken)
        {
            var positionAmount = await _context.Trades
                .Where(t => t.AssetId == assetId)
                .SumAsync(t => (decimal?)(t.Type == TradeType.Buy ? t.Quantity : -t.Quantity), cancellationToken) ?? 0;
            return positionAmount;
        }

        public Task<List<GetPositionsResponse>> GetPositionsAsync(CancellationToken cancellationToken)
        {
            return _context.Trades
                .AsNoTracking()
                .GroupBy(t => new
                {
                    t.AssetId,
                    t.Asset.Name,
                    t.Asset.Symbol
                })
                .Select(g => new GetPositionsResponse
                {
                    Quantity = g.Sum(t => t.Type == TradeType.Buy ? t.Quantity : -t.Quantity),
                    AssetId = g.Key.AssetId,
                    Name = g.Key.Name,
                    Symbol = g.Key.Symbol
                })
                .ToListAsync(cancellationToken);
        }
    }
}
