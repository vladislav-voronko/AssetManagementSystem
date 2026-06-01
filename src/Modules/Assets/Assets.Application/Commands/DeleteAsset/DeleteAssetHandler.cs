using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Assets.Application.Commands.DeleteAsset
{
    public class DeleteAssetHandler
    {
        private readonly BusinessDbContext _context;

        public DeleteAssetHandler(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteAssetCommand command, CancellationToken cancellationToken = default)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (asset == null)
            {
                return false;
            }

            asset.IsActive = false;
            asset.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
