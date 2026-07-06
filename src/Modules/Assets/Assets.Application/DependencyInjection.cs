using Assets.Application.Commands.CreateAsset;
using Assets.Application.Commands.SwitchAssetStatus;
using Assets.Application.Commands.UpdateAsset;
using Assets.Application.Queries.GetAssetById;
using Assets.Application.Queries.GetAssets;
using Microsoft.Extensions.DependencyInjection;

namespace Assets.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAssetsApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateAssetHandler>();
            services.AddScoped<UpdateAssetHandler>();
            services.AddScoped<SwitchAssetStatusHandler>();

            services.AddScoped<GetAssetsHandler>();
            services.AddScoped<GetAssetByIdHandler>();

            return services;
        }
    }
}
