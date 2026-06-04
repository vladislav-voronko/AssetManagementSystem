using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Db
{
    public interface IDbInitializer
    {
        Task MigrateAsync();
    }

    public interface IDbInitializer<TDbContext> : IDbInitializer
        where TDbContext : DbContext
    {
    }
}
