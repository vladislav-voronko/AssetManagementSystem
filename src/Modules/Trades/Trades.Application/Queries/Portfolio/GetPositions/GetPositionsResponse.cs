namespace Trades.Application.Queries.Portfolio.GetPositions
{
    public sealed class GetPositionsResponse
    {
        public Guid AssetId { get; init; }
        public string Symbol { get; init; } = null!;
        public string Name { get; init; } = null!;
        public decimal Quantity { get; init; }
    }
}
