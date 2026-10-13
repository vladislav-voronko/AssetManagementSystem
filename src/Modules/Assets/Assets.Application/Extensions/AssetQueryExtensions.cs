using Assets.Application.Queries.GetAssets;
using Assets.Domain.Entities;
using Shared.Application.Extensions;

namespace Assets.Application.Extensions
{
    public static class AssetQueryExtensions
    {
        public static IQueryable<Asset> ApplyFilters(this IQueryable<Asset> query, GetAssetsQuery filter)
        {
            return query
                .WhereIf(
                    !string.IsNullOrWhiteSpace(filter.Search),
                    a => a.Symbol.Contains(filter.Search!) ||
                    a.Name.Contains(filter.Search!))
                .WhereIf(
                    filter.Type.HasValue,
                    a => a.Type == filter.Type)
                .WhereIf(
                    filter.IsActive.HasValue,
                    a => a.IsActive == a.IsActive);
        }
    }
}
