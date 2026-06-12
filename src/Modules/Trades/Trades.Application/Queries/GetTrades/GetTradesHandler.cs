using BuildingBlocks.Application.Extensions;
using BuildingBlocks.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Extensions;
using Trades.Application.Projections;
using Trades.Contracts.Responses;

namespace Trades.Application.Queries.GetTrades
{
    public class GetTradesHandler
    {
        private readonly BusinessDbContext _context;
        public GetTradesHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TradeResponse>> Handle(GetTradesQuery query, CancellationToken cancellationToken)
        {
            return await _context.Trades
                .AsNoTracking()
                .ApplyFilters(query)
                .OrderByDescending(t => t.ExecutedAt)
                .Select(TradeProjection.ToResponse)
                .ToPagedResultAsync(query, cancellationToken);
        }
    }
}
