using Assets.Domain.Enums;

namespace Assets.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
