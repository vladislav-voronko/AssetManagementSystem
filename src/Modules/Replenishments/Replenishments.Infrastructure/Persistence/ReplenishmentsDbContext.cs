using Microsoft.EntityFrameworkCore;
using Replenishments.Application.Persistence;
using Replenishments.Domain.Entities;
using Shared.Infrastructure.Persistence;

namespace Replenishments.Infrastructure.Persistence
{
    public class ReplenishmentsDbContext : DbContext, IReplenishmentsDbContext
    {
        public ReplenishmentsDbContext(DbContextOptions<ReplenishmentsDbContext> options) : base(options) { }

        public DbSet<Replenishment> Replenishments => Set<Replenishment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schemas.AssetManagement);
            builder.ApplyConfigurationsFromAssembly(typeof(ReplenishmentsDbContext).Assembly);
        }
    }
}
