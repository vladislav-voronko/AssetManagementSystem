using Assets.Domain.Enums;

namespace Assets.Contracts.Responses
{
    public sealed class AssetResponse
    {
        public Guid Id { get; init; }
        public string Symbol { get; init; } = null!;
        public string Name { get; init; } = null!;
        public AssetType Type { get; init; }
        public bool IsActive { get; init; }
    }
}
