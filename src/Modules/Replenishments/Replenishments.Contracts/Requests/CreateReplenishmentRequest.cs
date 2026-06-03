namespace Replenishments.Contracts.Requests
{
    public sealed class CreateReplenishmentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime ReplenishedAt { get; set; }
        public string? Note { get; set; }
    }
}
