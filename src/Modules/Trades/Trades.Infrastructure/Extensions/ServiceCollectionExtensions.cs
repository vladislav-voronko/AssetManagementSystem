using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Db;
using Shared.Infrastructure.Persistence;
using Trades.Application.Persistence;
using Trades.Infrastructure.Persistence;

namespace Trades.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTradesDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradesDbContext>(options =>
                options.UseNpgsql(connectionString, npgsql =>
                        npgsql.MigrationsAssembly(typeof(TradesDbContext).Assembly.FullName)
                            .MigrationsHistoryTable("__trades_ef_migrations_history", Schemas.Infrastructure))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<ITradesDbContext>(serviceProvider =>
                serviceProvider.GetRequiredService<TradesDbContext>());
            services.AddScoped<IDbInitializer<TradesDbContext>, BaseDbInitializer<TradesDbContext>>();
            return services;
        }
    }
}
