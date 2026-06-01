using Assets.Application.Mappings;
using Assets.Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Assets.Application.Queries.GetAssets
{
    public sealed class GetAssetsHandler
    {
        private readonly BusinessDbContext _context;

        public GetAssetsHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<List<AssetResponse>> Handle(GetAssetsQuery query, CancellationToken cancellationToken)
        {
            var assets = await _context.Assets
                .AsNoTracking()
                .OrderBy(a => a.Symbol)
                .Select(a => a.ToResponse())
                .ToListAsync(cancellationToken);
            return assets;
        }
    }
}
