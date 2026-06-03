using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Mappings;
using Replenishments.Contracts.Responses;
using Replenishments.Domain.Entities;
using Shared.Infrastructure.Persistence;

namespace Replenishments.Application.Commands.CreateReplenishment
{
    public class CreateReplenishmentHandler
    {
        private readonly BusinessDbContext _context;

        public CreateReplenishmentHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<ReplenishmentResponse> Handle(CreateReplenishmentCommand command, CancellationToken cancellationToken)
        {
            var exists = await _context.Replenishments.AnyAsync(r => r.Amount == command.Amount && r.Currency == command.Currency && r.ReplenishedAt == command.ReplenishedAt);

            if (exists)
            {
                throw new InvalidOperationException("A replenishment with the same details already exists.");
            }

            var replenishment = new Replenishment
            {
                Id = Guid.NewGuid(),
                Amount = command.Amount,
                Currency = command.Currency,
                ReplenishedAt = command.ReplenishedAt,
                Note = command.Note,
                CreatedAt = DateTime.UtcNow
            };

            _context.Replenishments.Add(replenishment);
            await _context.SaveChangesAsync();

            return replenishment.ToResponse();
        }
    }
}
