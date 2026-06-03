namespace Replenishments.Application.Commands.CreateReplenishment
{
    public sealed record CreateReplenishmentCommand
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime ReplenishedAt { get; set; }
        public string? Note { get; set; }
    }
}
