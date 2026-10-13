using Assets.Application.Persistence;
using Assets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Persistence;

namespace Assets.Infrastructure.Persistence
{
    public class AssetsDbContext : DbContext, IAssetsDbContext
    {
        public AssetsDbContext(DbContextOptions<AssetsDbContext> options) : base(options) { }

        public DbSet<Asset> Assets => Set<Asset>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schemas.AssetManagement);
            builder.ApplyConfigurationsFromAssembly(typeof(AssetsDbContext).Assembly);
        }
    }
}
