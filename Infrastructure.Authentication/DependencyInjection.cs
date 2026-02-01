using Application.InfraInterfaces;
using Infrastructure.Authentication.Configurations;
using Infrastructure.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureAuthenticationServices(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            var jwtSettings = new JwtSettings();
            configurationManager.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
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
