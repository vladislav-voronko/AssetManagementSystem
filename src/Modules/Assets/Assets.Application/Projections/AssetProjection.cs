using Assets.Contracts.Responses;
using Assets.Domain.Entities;
using System.Linq.Expressions;

namespace Assets.Application.Projections
{
    public static class AssetProjection
    {
        public static Expression<Func<Asset, AssetResponse>> ToResponse =>
            asset => new AssetResponse
            {
                Id = asset.Id,
                Name = asset.Name,
                Symbol = asset.Symbol,
                Type = asset.Type,
                IsActive = asset.IsActive
            };
    }
}
