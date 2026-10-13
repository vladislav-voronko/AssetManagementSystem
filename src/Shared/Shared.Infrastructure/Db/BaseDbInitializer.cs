using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Db
{
    public class BaseDbInitializer<TDbContext> : IDbInitializer<TDbContext>
        where TDbContext : DbContext
    {
        public BaseDbInitializer(TDbContext context)
        {
            Context = context;
            Context.Database.SetCommandTimeout(120);
        }

        public TDbContext Context { get; protected set; }

        public async Task MigrateAsync()
        {
            try
            {
                var pendingMigrations = await Context.Database.GetPendingMigrationsAsync().ConfigureAwait(false);

                await Context.Database.MigrateAsync().ConfigureAwait(false);

                        await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while migrating the database.", ex);
            }
        }
    }
}
