using Assets.Application.Projections;
using Assets.Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Assets.Application.Queries.GetAssetById
{
    public sealed class GetAssetByIdHandler
    {
        private readonly BusinessDbContext _context;

        public GetAssetByIdHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<AssetResponse?> Handle(GetAssetByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.Assets
                .AsNoTracking()
                .Where(a => a.Id == query.Id)
                .Select(AssetProjection.ToResponse)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
