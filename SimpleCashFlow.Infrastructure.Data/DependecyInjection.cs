using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCashFlow.Application.Abstractions.Data;
using SimpleCashFlow.Domain.Abstractions.Repositories;
using SimpleCashFlow.Domain.Abstractions.Services;
using SimpleCashFlow.Domain.Services;
using SimpleCashFlow.Infrastructure.Data.Context;
using SimpleCashFlow.Infrastructure.Data.Repositories;

namespace SimpleCashFlow.Infrastructure.Data
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SimpleCashFlowContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("db")));

            services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<SimpleCashFlowContext>());

            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<ICashFlowDailySummaryRepository, CashFlowDailySummaryRepository>();
            services.AddScoped<ICalculateCashFlowDailySummary, CalculateCashFlowDailySummary>();

            return services;

        }
    }
}