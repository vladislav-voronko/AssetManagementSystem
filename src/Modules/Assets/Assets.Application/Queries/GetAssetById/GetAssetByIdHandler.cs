using Assets.Application.Mappings;
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
            var asset = await _context.Assets
                .AsNoTracking()
                .Where(a => a.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);
            return asset?.ToResponse();
        }
    }
}
