using Assets.Application;
using Assets.Infrastructure.Extensions;
using AssetManagement.Api.Infrastructure;
using Trades.Application;
using Trades.Application.Interfaces;
using Trades.Infrastructure.Extensions;
using Replenishments.Application;
using Replenishments.Infrastructure.Extensions;

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

            services.AddTradesDbContext(connectionString);
            services.AddAssetsDbContext(connectionString);
            services.AddReplenishmentsDbContext(connectionString);
            services.AddScoped<IReplenishmentBalanceReader, ReplenishmentBalanceReader>();
            services.AddScoped<IAssetLookup, AssetLookup>();

            return services;
        }
    }
}