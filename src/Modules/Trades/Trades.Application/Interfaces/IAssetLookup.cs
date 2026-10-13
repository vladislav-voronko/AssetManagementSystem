using Assets.Contracts.Responses;
using Assets.Domain.Enums;

namespace Trades.Application.Interfaces
{
    public interface IAssetLookup
    {
        Task<AssetResponse?> GetByIdAsync(Guid assetId, CancellationToken cancellationToken);
        Task<IReadOnlyDictionary<Guid, AssetResponse>> GetByIdsAsync(IReadOnlyCollection<Guid> assetIds, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Guid>> GetIdsByTypeAsync(AssetType assetType, CancellationToken cancellationToken);
    }
}
