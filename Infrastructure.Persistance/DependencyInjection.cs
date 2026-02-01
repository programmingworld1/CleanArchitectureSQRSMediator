using Application.InfraInterfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistanceServices(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configurationManager.GetConnectionString("SqlServer")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();

            return services;
        }
    }
}
