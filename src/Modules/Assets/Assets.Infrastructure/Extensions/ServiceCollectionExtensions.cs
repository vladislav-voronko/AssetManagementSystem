using Assets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Db;
using Shared.Infrastructure.Persistence;

namespace Assets.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAssetsDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AssetsDbContext>(options =>
                options.UseNpgsql(connectionString, npgsql =>
                        npgsql.MigrationsAssembly(typeof(AssetsDbContext).Assembly.FullName)
                            .MigrationsHistoryTable("__assets_ef_migrations_history", Schemas.Infrastructure))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<Assets.Application.Persistence.IAssetsDbContext>(serviceProvider =>
                serviceProvider.GetRequiredService<AssetsDbContext>());
            services.AddScoped<IDbInitializer<AssetsDbContext>, BaseDbInitializer<AssetsDbContext>>();
            return services;
        }
    }
}
