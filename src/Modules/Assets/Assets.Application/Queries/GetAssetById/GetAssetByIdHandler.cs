using Assets.Application.Projections;
using Assets.Contracts.Responses;
using Assets.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assets.Application.Queries.GetAssetById
{
    public sealed class GetAssetByIdHandler
    {
        private readonly IAssetsDbContext _context;

        public GetAssetByIdHandler(IAssetsDbContext context)
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
