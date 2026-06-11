using Assets.Domain.Enums;
using BuildingBlocks.Application.Filters;
using BuildingBlocks.Application.Pagination;
using Trades.Domain.Enums;

namespace Trades.Application.Queries.GetTrades
{
    public sealed record GetTradesQuery : PagedQuery, IDateRangeFilter
    {
        public Guid? AssetId { get; init; }
        public string? Search { get; init; }
        public TradeType? TradeType { get; init; }
        public AssetType? AssetType { get; init; }
        public string? Currency { get; init; }
        public bool? IsActive { get; init; }
        public DateTime? From { get; init; }
        public DateTime? To { get; init; }
    }
}
