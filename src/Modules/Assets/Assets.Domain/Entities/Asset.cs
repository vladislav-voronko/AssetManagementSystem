using Assets.Domain.Enums;
using Shared.Kernel.Abstractions;

namespace Assets.Domain.Entities
{
    public class Asset : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
