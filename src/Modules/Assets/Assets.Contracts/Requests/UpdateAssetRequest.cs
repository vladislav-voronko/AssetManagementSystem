using Assets.Domain.Enums;

namespace Assets.Contracts.Requests
{
    public sealed class UpdateAssetRequest
    {
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
