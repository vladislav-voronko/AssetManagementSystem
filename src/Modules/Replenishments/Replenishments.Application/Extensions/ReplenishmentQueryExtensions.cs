using BuildingBlocks.Application.Extensions;
using Replenishments.Application.Queries.GetReplenishments;
using Replenishments.Domain.Entities;

namespace Replenishments.Application.Extensions
{
    public static class ReplenishmentQueryExtensions
    {
        public static IQueryable<Replenishment> ApplyFilters(this IQueryable<Replenishment> query, GetReplenishmentsQuery filter)
        {
            return query
                .WhereIf(
                    !string.IsNullOrWhiteSpace(filter.Currency),
                    r => r.Currency.Contains(filter.Currency!))
                .WhereIf(
                    filter.From.HasValue,
                    r => r.ReplenishedAt >= filter.From)
                .WhereIf(
                    filter.To.HasValue,
                    r => r.ReplenishedAt <= filter.To);
        }
    }
}
