using Assets.Application.Mappings;
using Assets.Contracts.Responses;
using Assets.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assets.Application.Commands.SwitchAssetStatus
{
    public class SwitchAssetStatusHandler
    {
        private readonly IAssetsDbContext _context;

        public SwitchAssetStatusHandler(IAssetsDbContext context)
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
