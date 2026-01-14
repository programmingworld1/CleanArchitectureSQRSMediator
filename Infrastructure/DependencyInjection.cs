using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Infrastructure.Configurations;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IGitHubClient, GitHubClient>();

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configurationManager.GetConnectionString("SqlServer")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();

            var jwtSettings = new JwtSettings();
            configurationManager.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
    }
}
