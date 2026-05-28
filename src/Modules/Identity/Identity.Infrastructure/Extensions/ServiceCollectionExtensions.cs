using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
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

        public static void ApplySnakeCaseIdentityNaming(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (clrType == null)
                    continue;

                // Identity tables only
                if (!typeof(IdentityUser<Guid>).IsAssignableFrom(clrType) &&
                    !typeof(IdentityRole<Guid>).IsAssignableFrom(clrType) &&
                    !clrType.Name.Contains("Identity"))
                {
                    continue;
                }

                entityType.SetTableName(ToSnakeCase(entityType.GetTableName()!));
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = System.Text.RegularExpressions.Regex
                .Replace(input, "([a-z0-9])([A-Z])", "$1_$2")
                .ToLower();

            return result;
        }
    }
}
