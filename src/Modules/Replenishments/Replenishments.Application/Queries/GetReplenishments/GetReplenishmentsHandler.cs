using Shared.Application.Extensions;
using Shared.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Extensions;
using Replenishments.Application.Projections;
using Replenishments.Contracts.Responses;
using Replenishments.Application.Persistence;

namespace Replenishments.Application.Queries.GetReplenishments
{
    public class GetReplenishmentsHandler
    {
        private readonly IReplenishmentsDbContext _context;

        public GetReplenishmentsHandler(IReplenishmentsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReplenishmentResponse>> Handle(GetReplenishmentsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Replenishments
                .AsNoTracking()
                .ApplyFilters(query)
                .OrderByDescending(r => r.CreatedAt)
                .Select(ReplenishmentProjection.ToResponse)
                .ToPagedResultAsync(query, cancellationToken);
        }
    }
}
