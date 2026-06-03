using Assets.Domain.Enums;

namespace Assets.Application.Commands.CreateAsset
{
    public sealed record CreateAssetCommand
    {
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; }
    }
}
