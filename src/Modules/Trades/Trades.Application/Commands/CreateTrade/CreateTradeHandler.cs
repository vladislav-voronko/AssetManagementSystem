using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Projections;
using Trades.Contracts.Responses;
using Trades.Domain.Entities;

namespace Trades.Application.Commands.CreateTrade
{
    public class CreateTradeHandler
    {
        private readonly BusinessDbContext _context;

        public CreateTradeHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<TradeResponse> Handle(CreateTradeCommand command, CancellationToken cancellationToken)
        {
            var exists = await _context.Trades.AnyAsync(x => x.AssetId == command.AssetId && x.ExecutedAt == command.ExecutedAt);

            if (exists)
            {
                throw new InvalidOperationException($"Trade already exists.");
            }

            var trade = new Trade
            {
                Id = Guid.NewGuid(),
                AssetId = command.AssetId,
                Type = command.Type,
                Quantity = command.Quantity,
                Price = command.Price,
                Currency = command.Currency,
                ExecutedAt = command.ExecutedAt,
                CreatedAt = DateTime.UtcNow
            };

            _context.Trades.Add(trade);
            await _context.SaveChangesAsync(cancellationToken);

            return await _context.Trades
                .AsNoTracking()
                .Where(t => t.Id == trade.Id)
                .Select(TradeProjection.ToResponse)
                .FirstAsync(cancellationToken);
        }
    }
}
