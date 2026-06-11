using Trades.Domain.Enums;

namespace Trades.Contracts.Requests
{
    public sealed class CreateTradeRequest
    {
        public Guid AssetId { get; set; }
        public TradeType Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime ExecutedAt { get; set; }
    }
}
