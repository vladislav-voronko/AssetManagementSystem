using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Projections;
using Replenishments.Contracts.Responses;
using Replenishments.Application.Persistence;

namespace Replenishments.Application.Queries.GetReplenishmentById
{
    public class GetReplenishmentByIdHandler
    {
        private readonly IReplenishmentsDbContext _context;

        public GetReplenishmentByIdHandler(IReplenishmentsDbContext context)
        {
            _context = context;
        }

        public async Task<ReplenishmentResponse?> Handle(GetReplenishmentByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.Replenishments
                .AsNoTracking()
                .Where(r => r.Id == query.Id)
                .Select(ReplenishmentProjection.ToResponse)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
