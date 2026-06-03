using Assets.Contracts.Responses;
using System.Linq.Expressions;
using Trades.Contracts.Responses;
using Trades.Domain.Entities;

namespace Trades.Application.Projections
{
    public static class TradeProjection
    {
        public static Expression<Func<Trade, TradeResponse>> ToResponse =>
            trade => new TradeResponse
            {
                Id = trade.Id,
                AssetId = trade.AssetId,
                Type = trade.Type,
                Quantity = trade.Quantity,
                Price = trade.Price,
                Currency = trade.Currency,
                ExecutedAt = trade.ExecutedAt,
                Asset = new AssetResponse
                {
                    Id = trade.Asset.Id,
                    Name = trade.Asset.Name,
                    Symbol = trade.Asset.Symbol,
                    Type = trade.Asset.Type,
                    IsActive = trade.Asset.IsActive
                }
            };
    }
}
