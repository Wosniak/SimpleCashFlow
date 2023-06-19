using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleCashFlow.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssemby = typeof(DependecyInjection).Assembly;

            services.AddMediatR(config => config.RegisterServicesFromAssembly(currentAssemby));

            services.AddValidatorsFromAssembly(currentAssemby);

            return services;

        }
    }
}