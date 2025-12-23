using Application.Interfaces;
using Application.Persistance;
using Infrastructure.Configurations;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            ConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configurationManager.GetConnectionString("SqlServer")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBurgerRepository, BurgerRepository>();

            return services;
        }
    }
}
