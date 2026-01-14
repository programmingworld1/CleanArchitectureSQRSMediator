using Infrastructure.Services;
using Rockstar.Mappings;

namespace Rockstar
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddHttpClient<GitHubClient>()
                .AddStandardResilienceHandler(options =>
                {
                    options.Retry.MaxRetryAttempts = 3;
                    options.Retry.Delay = TimeSpan.FromSeconds(1);
                });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            return services;
        }
    }
}
