using Assets.Application.Extensions;
using Assets.Application.Projections;
using Assets.Contracts.Responses;
using Assets.Application.Persistence;
using Shared.Application.Extensions;
using Shared.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Assets.Application.Queries.GetAssets
{
    public sealed class GetAssetsHandler
    {
        private readonly IAssetsDbContext _context;

        public GetAssetsHandler(IAssetsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<AssetResponse>> Handle(GetAssetsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Assets
                .AsNoTracking()
                .ApplyFilters(query)
                .OrderBy(a => a.Symbol)
                .Select(AssetProjection.ToResponse)
                .ToPagedResultAsync(query, cancellationToken);
        }
    }
}
