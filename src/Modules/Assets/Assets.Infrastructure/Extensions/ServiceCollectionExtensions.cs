using Assets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Db;
using SharedKernel.Persistence;

namespace Assets.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessDbInitializer(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer<BusinessDbContext>, BaseDbInitializer<BusinessDbContext>>();
            return services;
        }

        public static IServiceCollection AddBusinessDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BusinessDbContext>(options =>
                options.UseNpgsql(connectionString,
                        npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", Schemas.Infrastructure))
                    .UseSnakeCaseNamingConvention());

            return services;
        }
    }
}
