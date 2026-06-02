using Trades.Contracts.Responses;
using Trades.Domain.Entities;
using Assets.Contracts.Responses;

namespace Trades.Application.Mappings
{
    public static class TradeMappings
    {
        public static TradeResponse ToResponse(this Trade trade)
        {
            return new TradeResponse
            {
                Id = trade.Id,
                AssetId = trade.AssetId,
                Type = trade.Type,
                Quantity = trade.Quantity,
                Price = trade.Price,
                ExecutedAt = trade.ExecutedAt,
                Asset = new AssetResponse
                {
                    Id = trade.Asset.Id,
                    Name = trade.Asset.Name,
                    Symbol = trade.Asset.Symbol,
                    Type = trade.Asset.Type,
                    Currency = trade.Asset.Currency,
                    IsActive = trade.Asset.IsActive
                }
            };
        } 
    }
}
