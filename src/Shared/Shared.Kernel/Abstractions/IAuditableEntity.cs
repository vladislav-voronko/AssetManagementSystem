namespace Shared.Kernel.Abstractions
{
    public interface IAuditableEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
