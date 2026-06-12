using Assets.Application.Extensions;
using Assets.Application.Projections;
using Assets.Contracts.Responses;
using BuildingBlocks.Application.Extensions;
using BuildingBlocks.Application.Pagination;
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
