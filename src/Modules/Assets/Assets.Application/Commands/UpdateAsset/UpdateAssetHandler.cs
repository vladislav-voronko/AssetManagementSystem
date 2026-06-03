using Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Assets.Contracts.Responses;
using Assets.Application.Mappings;

namespace Assets.Application.Commands.UpdateAsset
{
    public class UpdateAssetHandler
    {
        private readonly BusinessDbContext _context;

        public UpdateAssetHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<AssetResponse?> Handle(UpdateAssetCommand command, CancellationToken cancellationToken = default)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (asset == null) 
            {
                return null;
            }

            asset.Name = command.Name;
            asset.Symbol = command.Symbol;
            asset.Type = command.Type;
            asset.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return asset.ToResponse();
        }
    }
}
