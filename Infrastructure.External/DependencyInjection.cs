using Application.InfraInterfaces;
using Infrastructure.External.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.External
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureExternalServices(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.AddScoped<IGitHubClient, GitHubClient>();

            services.AddHttpClient<GitHubClient>()
                .AddStandardResilienceHandler(options =>
                {
                    options.Retry.MaxRetryAttempts = 3;
                    options.Retry.Delay = TimeSpan.FromSeconds(1);
                });

            return services;
        }
    }
}
