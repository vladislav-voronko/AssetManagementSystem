using Assets.Infrastructure.Extensions;
using Identity.Infrastructure.Extensions;

namespace AssetManagement.Api.Extensions
{
    public static class ApplicationExtensions
    {

        public static IServiceCollection AddDbInitializers(this IServiceCollection services)
        {
            services.AddBusinessDbInitializer();
            services.AddIdentityDbInitializer();

            return services;
        }

        public static void AddServiceLinesDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddBusinessDbContext(connectionString);
            services.AddIdentityDbContext(connectionString);
        }
    }
}
