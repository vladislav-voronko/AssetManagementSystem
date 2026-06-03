using Assets.Application;
using Trades.Application;
using Identity.Infrastructure.Extensions;
using Replenishments.Application;
using Shared.Infrastructure.Extensions;

namespace AssetManagement.Api.Extensions
{
    public static class ModuleRegistrationExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            services.AddAssetsApplication();
            services.AddTradesApplication();
            services.AddReplenishmentsApplication();

            return services;
        }

        private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            services.AddBusinessDbContext(connectionString);
            services.AddIdentityDbContext(connectionString);

            services.AddBusinessDbInitializer();
            services.AddIdentityDbInitializer();

            return services;
        }
    }
}