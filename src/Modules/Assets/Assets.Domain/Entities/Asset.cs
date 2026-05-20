namespace Assets.Domain.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
