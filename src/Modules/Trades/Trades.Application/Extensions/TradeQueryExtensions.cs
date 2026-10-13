using Shared.Application.Extensions;
using Trades.Application.Queries.GetTrades;
using Trades.Domain.Entities;

namespace Trades.Application.Extensions
{
    public static class TradeQueryExtensions
    {
        public static IQueryable<Trade> ApplyFilters(this IQueryable<Trade> query, GetTradesQuery filter)
        {
            return query
                .WhereIf(
                    filter.AssetId.HasValue,
                    t => t.AssetId == filter.AssetId)
                .WhereIf(
                    filter.TradeType.HasValue,
                    t => t.Type == filter.TradeType)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(filter.Currency),
                    t => t.Currency == filter.Currency)
                .WhereIf(
                    filter.From.HasValue,
                    t => t.ExecutedAt >= filter.From)
                .WhereIf(
                    filter.To.HasValue,
                    t => t.ExecutedAt <= filter.To);
        }
    }
}