using BuildingBlocks.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Extensions;
using Replenishments.Application.Projections;
using Replenishments.Contracts.Responses;
using Shared.Infrastructure.Persistence;

namespace Replenishments.Application.Queries.GetReplenishments
{
    public class GetReplenishmentsHandler
    {
        private readonly BusinessDbContext _context;

        public GetReplenishmentsHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReplenishmentResponse>> Handle(GetReplenishmentsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Replenishments
                .AsNoTracking()
                .ApplyFilters(query)
                .OrderByDescending(r => r.CreatedAt)
                .ApplyPaging(query.Page, query.PageSize)
                .Select(ReplenishmentProjection.ToResponse)
                .ToListAsync(cancellationToken);
        }
    }
}
