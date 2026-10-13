using Assets.Application.Persistence;
using Assets.Contracts.Responses;
using Assets.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Trades.Application.Interfaces;

namespace AssetManagement.Api.Infrastructure
{
    public sealed class AssetLookup : IAssetLookup
    {
        private readonly IAssetsDbContext _context;

        public AssetLookup(IAssetsDbContext context)
        {
            _context = context;
        }

        public Task<AssetResponse?> GetByIdAsync(Guid assetId, CancellationToken cancellationToken)
        {
            return _context.Assets
                .AsNoTracking()
                .Where(asset => asset.Id == assetId)
                .Select(asset => new AssetResponse
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                    Type = asset.Type,
                    IsActive = asset.IsActive
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyDictionary<Guid, AssetResponse>> GetByIdsAsync(
            IReadOnlyCollection<Guid> assetIds,
            CancellationToken cancellationToken)
        {
            var ids = assetIds.Distinct().ToArray();
            if (ids.Length == 0)
            {
                return new Dictionary<Guid, AssetResponse>();
            }

            return await _context.Assets
                .AsNoTracking()
                .Where(asset => ids.Contains(asset.Id))
                .Select(asset => new AssetResponse
                {
                    Id = asset.Id,
                    Name = asset.Name,
                    Symbol = asset.Symbol,
                    Type = asset.Type,
                    IsActive = asset.IsActive
                })
                .ToDictionaryAsync(asset => asset.Id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Guid>> GetIdsByTypeAsync(AssetType assetType, CancellationToken cancellationToken)
        {
            return await _context.Assets
                .AsNoTracking()
                .Where(asset => asset.Type == assetType)
                .Select(asset => asset.Id)
                .ToListAsync(cancellationToken);
        }
    }
}
