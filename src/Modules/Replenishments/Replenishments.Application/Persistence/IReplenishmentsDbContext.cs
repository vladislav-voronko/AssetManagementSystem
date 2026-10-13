using Microsoft.EntityFrameworkCore;
using Replenishments.Domain.Entities;

namespace Replenishments.Application.Persistence
{
    public interface IReplenishmentsDbContext
    {
        DbSet<Replenishment> Replenishments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
