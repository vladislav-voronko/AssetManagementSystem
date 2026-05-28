using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Db
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

                if (pendingMigrations.Any())
                {
                    Context.Database.SetCommandTimeout(900);
                    await Context.Database.MigrateAsync().ConfigureAwait(false);

                    var executionStrategy = Context.Database.CreateExecutionStrategy();
                    await executionStrategy.ExecuteAsync(async
                        () =>
                    {
                        using var tr = Context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
                        await Context.SaveChangesAsync().ConfigureAwait(false);
                        tr.Commit();
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while migrating the database.", ex);
            }
        }
    }
}
