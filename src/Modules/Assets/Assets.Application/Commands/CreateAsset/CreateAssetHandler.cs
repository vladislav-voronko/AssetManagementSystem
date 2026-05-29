using Assets.Domain.Entities;
using Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assets.Application.Commands.CreateAsset
{
    public class CreateAssetHandler
    {
        private readonly BusinessDbContext _context;

        public CreateAssetHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAssetCommand command, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Assets.AnyAsync(x => x.Symbol == command.Symbol, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException($"Asset '{command.Symbol}' already exists.");
            }

            var asset = new Asset
            {
                Id = Guid.NewGuid(),
                Symbol = command.Symbol,
                Name = command.Name,
                Type = command.Type,
                Currency = command.Currency,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Assets.Add(asset);

            await _context.SaveChangesAsync(cancellationToken);

            return asset.Id;
        }
    }
}
