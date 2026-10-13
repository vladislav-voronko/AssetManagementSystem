using Trades.Contracts.Responses;
using Trades.Domain.Entities;
using Assets.Contracts.Responses;

namespace Trades.Application.Mappings
{
    public static class TradeMappings
    {
        public static TradeResponse ToResponse(this Trade trade, AssetResponse asset)
        {
            return new TradeResponse
            {
                Id = trade.Id,
                AssetId = trade.AssetId,
                Type = trade.Type,
                Quantity = trade.Quantity,
                Price = trade.Price,
                Currency = trade.Currency,
                ExecutedAt = trade.ExecutedAt,
                Asset = asset
            };
        } 
    }
}
