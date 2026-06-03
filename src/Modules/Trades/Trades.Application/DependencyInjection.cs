using Microsoft.Extensions.DependencyInjection;
using Trades.Application.Commands.CreateTrade;
using Trades.Application.Interfaces;
using Trades.Application.Queries.GetTradeById;
using Trades.Application.Queries.GetTrades;
using Trades.Application.Queries.Portfolio.GetCashBalance;
using Trades.Application.Services;

namespace Trades.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTradesApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateTradeHandler>();

            services.AddScoped<GetTradesHandler>();
            services.AddScoped<GetTradeByIdHandler>();

            services.AddScoped<GetCashBalanceHandler>();
            services.AddScoped<ICashBalanceService, CashBalanceService>();

            return services;
        }
    }
}
