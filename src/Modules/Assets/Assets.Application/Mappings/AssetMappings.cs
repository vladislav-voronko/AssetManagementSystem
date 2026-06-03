using Assets.Contracts.Responses;
using Assets.Domain.Entities;

namespace Assets.Application.Mappings
{
    public static class AssetMappings
    {
        public static AssetResponse ToResponse(this Asset asset)
        {
            return new AssetResponse
            {
                Id = asset.Id,
                Symbol = asset.Symbol,
                Name = asset.Name,
                Type = asset.Type,
                IsActive = asset.IsActive
            };
        }
    }
}
