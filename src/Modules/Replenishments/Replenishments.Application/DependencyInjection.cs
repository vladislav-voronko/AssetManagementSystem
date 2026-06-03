using Microsoft.Extensions.DependencyInjection;
using Replenishments.Application.Commands.CreateReplenishment;
using Replenishments.Application.Queries.GetReplenishmentById;
using Replenishments.Application.Queries.GetReplenishments;

namespace Replenishments.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReplenishmentsApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateReplenishmentHandler>();

            services.AddScoped<GetReplenishmentsHandler>();
            services.AddScoped<GetReplenishmentByIdHandler>();

            return services;
        }
    }
}
