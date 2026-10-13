using Microsoft.EntityFrameworkCore;
using Trades.Application.Interfaces;
using Trades.Application.Mappings;
using Trades.Application.Persistence;
using Trades.Contracts.Responses;

namespace Trades.Application.Queries.GetTradeById
{
    public class GetTradeByIdHandler
    {
        private readonly ITradesDbContext _context;
        private readonly IAssetLookup _assetLookup;

        public GetTradeByIdHandler(ITradesDbContext context, IAssetLookup assetLookup)
        {
            _context = context;
            _assetLookup = assetLookup;
        }

        public async Task<TradeResponse?> Handle(GetTradeByIdQuery query, CancellationToken cancellationToken)
        {
            var trade = await _context.Trades
                .AsNoTracking()
                .Where(t => t.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (trade is null)
            {
                return null;
            }

            var asset = await _assetLookup.GetByIdAsync(trade.AssetId, cancellationToken);
            if (asset is null)
            {
                throw new InvalidOperationException($"Asset '{trade.AssetId}' referenced by trade '{trade.Id}' was not found.");
            }

            return trade.ToResponse(asset);
        }
    }
}
