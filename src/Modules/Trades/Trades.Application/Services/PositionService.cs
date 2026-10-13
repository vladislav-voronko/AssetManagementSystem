using Microsoft.EntityFrameworkCore;
using Trades.Application.Interfaces;
using Trades.Application.Persistence;
using Trades.Application.Queries.Portfolio.GetPositions;
using Trades.Domain.Enums;

namespace Trades.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly ITradesDbContext _context;
        private readonly IAssetLookup _assetLookup;

        public PositionService(ITradesDbContext context, IAssetLookup assetLookup)
        {
            _context = context;
            _assetLookup = assetLookup;
        }

        public async Task<decimal> GetPositionAsync(Guid assetId, CancellationToken cancellationToken)
        {
            var positionAmount = await _context.Trades
                .Where(t => t.AssetId == assetId)
                .SumAsync(t => (decimal?)(t.Type == TradeType.Buy ? t.Quantity : -t.Quantity), cancellationToken) ?? 0;
            return positionAmount;
        }

        public async Task<List<GetPositionsResponse>> GetPositionsAsync(CancellationToken cancellationToken)
        {
            var positions = await _context.Trades
                .AsNoTracking()
                .GroupBy(trade => trade.AssetId)
                .Select(g => new GetPositionsResponse
                {
                    Quantity = g.Sum(t => t.Type == TradeType.Buy ? t.Quantity : -t.Quantity),
                    AssetId = g.Key
                })
                .ToListAsync(cancellationToken);

            var assets = await _assetLookup.GetByIdsAsync(positions.Select(position => position.AssetId).ToArray(), cancellationToken);

            return positions.Select(position =>
            {
                if (!assets.TryGetValue(position.AssetId, out var asset))
                {
                    throw new InvalidOperationException($"Asset '{position.AssetId}' referenced by a trade was not found.");
                }

                return new GetPositionsResponse
                {
                    AssetId = position.AssetId,
                    Quantity = position.Quantity,
                    Name = asset.Name,
                    Symbol = asset.Symbol
                };
            }).ToList();
        }
    }
}
