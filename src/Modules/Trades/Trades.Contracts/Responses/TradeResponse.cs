using Assets.Contracts.Responses;
using Trades.Domain.Enums;

namespace Trades.Contracts.Responses
{
    public sealed class TradeResponse
    {
        public Guid Id { get; init; }
        public Guid AssetId { get; init; }
        public TradeType Type { get; init; }
        public decimal Quantity { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; } = null!;
        public DateTime ExecutedAt { get; init; }
        public AssetResponse Asset { get; init; } = null!;
    }
}
