using Assets.Application.Mappings;
using Assets.Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Assets.Application.Commands.SwitchAssetStatus
{
    public class SwitchAssetStatusHandler
    {
        private readonly BusinessDbContext _context;

        public SwitchAssetStatusHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<AssetResponse?> Handle(SwitchAssetStatusCommand command, CancellationToken cancellationToken = default)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (asset == null)
            {
                return null;
            }

            asset.IsActive = command.IsActive;
            asset.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return asset.ToResponse();
        }
    }
}
