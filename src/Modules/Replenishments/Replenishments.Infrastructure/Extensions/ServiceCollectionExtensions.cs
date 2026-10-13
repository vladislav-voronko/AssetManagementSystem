using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Replenishments.Application.Persistence;
using Replenishments.Infrastructure.Persistence;
using Shared.Infrastructure.Db;
using Shared.Infrastructure.Persistence;

namespace Replenishments.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReplenishmentsDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ReplenishmentsDbContext>(options =>
                options.UseNpgsql(connectionString, npgsql =>
                        npgsql.MigrationsAssembly(typeof(ReplenishmentsDbContext).Assembly.FullName)
                            .MigrationsHistoryTable("__replenishments_ef_migrations_history", Schemas.Infrastructure))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IReplenishmentsDbContext>(serviceProvider =>
                serviceProvider.GetRequiredService<ReplenishmentsDbContext>());
            services.AddScoped<IDbInitializer<ReplenishmentsDbContext>, BaseDbInitializer<ReplenishmentsDbContext>>();
            return services;
        }
    }
}
