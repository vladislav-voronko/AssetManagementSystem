using Shared.Application.Extensions;
using Shared.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Trades.Application.Interfaces;
using Trades.Application.Persistence;
using Trades.Application.Extensions;
using Trades.Application.Mappings;
using Trades.Contracts.Responses;

namespace Trades.Application.Queries.GetTrades
{
    public class GetTradesHandler
    {
        private readonly ITradesDbContext _context;
        private readonly IAssetLookup _assetLookup;

        public GetTradesHandler(ITradesDbContext context, IAssetLookup assetLookup)
        {
            _context = context;
            _assetLookup = assetLookup;
        }

        public async Task<PagedResult<TradeResponse>> Handle(GetTradesQuery query, CancellationToken cancellationToken)
        {
            var tradesQuery = _context.Trades
                .AsNoTracking()
                .ApplyFilters(query);

            if (query.AssetType.HasValue)
            {
                var assetIds = (await _assetLookup.GetIdsByTypeAsync(query.AssetType.Value, cancellationToken)).ToArray();
                tradesQuery = tradesQuery.Where(trade => assetIds.Contains(trade.AssetId));
            }

            var page = await tradesQuery
                .OrderByDescending(trade => trade.ExecutedAt)
                .ToPagedResultAsync(query, cancellationToken);

            var assets = await _assetLookup.GetByIdsAsync(page.Items.Select(trade => trade.AssetId).ToArray(), cancellationToken);
            var items = page.Items.Select(trade =>
            {
                if (!assets.TryGetValue(trade.AssetId, out var asset))
                {
                    throw new InvalidOperationException($"Asset '{trade.AssetId}' referenced by trade '{trade.Id}' was not found.");
                }

                return trade.ToResponse(asset);
            }).ToArray();

            return new PagedResult<TradeResponse>
            {
                Items = items,
                TotalCount = page.TotalCount,
                Page = page.Page,
                PageSize = page.PageSize
            };
        }
    }
}
