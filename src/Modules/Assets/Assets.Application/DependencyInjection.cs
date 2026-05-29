using Assets.Application.Commands.CreateAsset;
using Microsoft.Extensions.DependencyInjection;

namespace Assets.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAssetsApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateAssetHandler>();

            return services;
        }
    }
}
