using Microsoft.EntityFrameworkCore;
using Trades.Domain.Entities;

namespace Trades.Application.Persistence
{
    public interface ITradesDbContext
    {
        DbSet<Trade> Trades { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
