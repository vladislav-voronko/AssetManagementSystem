using Assets.Domain.Enums;

namespace Assets.Application.Commands.UpdateAsset
{
    public sealed record UpdateAssetCommand
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; }
        public bool IsActive { get; set; }
    }
}