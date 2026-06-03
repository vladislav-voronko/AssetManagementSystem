namespace Replenishments.Contracts.Responses
{
    public sealed class ReplenishmentResponse
    {
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public string Currency { get; init; } = null!;
        public DateTime ReplenishedAt { get; init; }
        public string? Note { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
