using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Db
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
