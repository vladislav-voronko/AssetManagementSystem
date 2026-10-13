using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;
using Trades.Application.Persistence;
using Trades.Domain.Entities;

namespace Trades.Infrastructure.Persistence
{
    public class TradesDbContext : DbContext, ITradesDbContext
    {
        public TradesDbContext(DbContextOptions<TradesDbContext> options) : base(options) { }

        public DbSet<Trade> Trades => Set<Trade>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schemas.AssetManagement);
            builder.ApplyConfigurationsFromAssembly(typeof(TradesDbContext).Assembly);
        }
    }
}
