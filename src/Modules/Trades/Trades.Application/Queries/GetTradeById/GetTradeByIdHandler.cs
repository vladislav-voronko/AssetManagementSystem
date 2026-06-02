using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Projections;
using Trades.Contracts.Responses;

namespace Trades.Application.Queries.GetTradeById
{
    public class GetTradeByIdHandler
    {
        private readonly BusinessDbContext _context;

        public GetTradeByIdHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<TradeResponse?> Handle(GetTradeByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.Trades
                .AsNoTracking()
                .Where(t => t.Id == query.Id)
                .Select(TradeProjection.ToResponse)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
