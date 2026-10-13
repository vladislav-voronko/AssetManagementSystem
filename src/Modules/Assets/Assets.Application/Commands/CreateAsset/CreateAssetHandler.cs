using Assets.Domain.Entities;
using Assets.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Assets.Contracts.Responses;
using Assets.Application.Mappings;

namespace Assets.Application.Commands.CreateAsset
{
    public class CreateAssetHandler
    {
        private readonly IAssetsDbContext _context;

        public CreateAssetHandler(IAssetsDbContext context)
        {
            _context = context;
        }

        public async Task<AssetResponse> Handle(CreateAssetCommand command, CancellationToken cancellationToken = default)
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
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _context.Assets.Add(asset);

            await _context.SaveChangesAsync(cancellationToken);

            return asset.ToResponse();
        }
    }
}
