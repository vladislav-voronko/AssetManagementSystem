using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Db;
using SharedKernel.Persistence;

namespace Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityDbInitializer(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer<IdentityDbContext>, BaseDbInitializer<IdentityDbContext>>();
            return services;
        }

        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(connectionString,
                        npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", Schemas.Infrastructure))
                    .UseSnakeCaseNamingConvention());

            return services;
        }
    }
}
