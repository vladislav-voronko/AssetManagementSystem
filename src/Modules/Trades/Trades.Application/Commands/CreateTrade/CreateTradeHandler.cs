using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Interfaces;
using Trades.Application.Projections;
using Trades.Contracts.Responses;
using Trades.Domain.Entities;
using Trades.Domain.Enums;

namespace Trades.Application.Commands.CreateTrade
{
    public class CreateTradeHandler
    {
        private readonly BusinessDbContext _context;
        private readonly ICashBalanceService _cashBalanceService;
        private readonly IPositionService _positionService;

        public CreateTradeHandler(BusinessDbContext context, ICashBalanceService cashBalanceService, IPositionService positionService)
        {
            _context = context;
            _cashBalanceService = cashBalanceService;
            _positionService = positionService;
        }

        public async Task<TradeResponse> Handle(CreateTradeCommand command, CancellationToken cancellationToken)
        {
            var exists = await _context.Trades.AnyAsync(x => x.AssetId == command.AssetId && x.ExecutedAt == command.ExecutedAt, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException($"Trade already exists.");
            }

            var requestedTradeAmount = command.Quantity * command.Price;

            if (command.Type == TradeType.Buy)
            {
                var balance = await _cashBalanceService.GetBalanceAsync(
                    command.Currency,
                    cancellationToken);

                if (balance.Balance < requestedTradeAmount)
                {
                    throw new InvalidOperationException(
                        $"Insufficient {command.Currency} balance.");
                }
            }
            else if (command.Type == TradeType.Sell)
            {
                var assetBalance = await _positionService.GetPositionAsync(
                    command.AssetId,
                    cancellationToken);
                if (assetBalance < command.Quantity)
                {
                    throw new InvalidOperationException(
                        $"Insufficient asset balance.");
                }
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
