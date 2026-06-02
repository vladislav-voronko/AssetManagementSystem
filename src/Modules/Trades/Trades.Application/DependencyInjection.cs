using Microsoft.Extensions.DependencyInjection;
using Trades.Application.Commands.CreateTrade;
using Trades.Application.Queries.GetTradeById;
using Trades.Application.Queries.GetTrades;

namespace Trades.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTradesApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateTradeHandler>();

            services.AddScoped<GetTradesHandler>();
            services.AddScoped<GetTradeByIdHandler>();

            return services;
        }
    }
}
