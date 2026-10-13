using Assets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assets.Application.Persistence
{
    public interface IAssetsDbContext
    {
        DbSet<Asset> Assets { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
