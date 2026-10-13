using Shared.Application.Filters;
using Shared.Application.Pagination;

namespace Replenishments.Application.Queries.GetReplenishments
{
    public sealed record GetReplenishmentsQuery : PagedQuery, IDateRangeFilter
    {
        public string? Currency { get; init; }
        public DateTime? From { get; init; }
        public DateTime? To { get; init; }
    }
}
