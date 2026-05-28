using Assets.Domain.Entities;
using Trades.Domain.Entities;
using Replenishments.Domain.Entities;
using SharedKernel.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Persistence
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions<BusinessDbContext> options) : base(options) { }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Replenishment> Replenishments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            builder.HasDefaultSchema(Schemas.AssetManagement); 
            
            builder.ApplyConfigurationsFromAssembly(typeof(BusinessDbContext).Assembly);
        }
    }
}
