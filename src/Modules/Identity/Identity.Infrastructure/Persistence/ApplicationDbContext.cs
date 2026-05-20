using Assets.Domain.Entities;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Replenishments.Domain.Entities;
using Trades.Domain.Entities;

namespace Identity.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Replenishment> Replenishments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(Schemas.AssetManagement);

            builder.Entity<ApplicationUser>()
                .ToTable("users", Schemas.Identity);

            builder.Entity<IdentityRole<Guid>>()
                .ToTable("roles", Schemas.Identity);

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("user_roles", Schemas.Identity);

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("user_claims", Schemas.Identity);

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("user_logins", Schemas.Identity);

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("role_claims", Schemas.Identity);

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("user_tokens", Schemas.Identity);
        }
    }
}