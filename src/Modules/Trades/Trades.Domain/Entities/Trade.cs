using Assets.Domain.Entities;
using Trades.Domain.Enums;

namespace Trades.Domain.Entities
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid AssetId { get; set; }
        public TradeType Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public DateTime ExecutedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Asset Asset { get; set; } = null!;
    }
}
