using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisticFy.Domain.Repositories;
using MisticFy.Domain.Repositories.Playlists;
using MisticFy.Domain.Repositories.Search;
using MisticFy.Domain.Services;
using MisticFy.Domain.Services.ProfileValidator;
using MisticFy.Domain.Services.TokenServices;
using MisticFy.Infrastructure.DataAcess.Context;
using MisticFy.Infrastructure.DataAcess.Repositories;
using MisticFy.Infrastructure.DataAcess.Services;
using MisticFy.Infrastructure.DataAcess.Services.ProfileValidators;
using MisticFy.Infrastructure.DataAcess.Services.TokenServices;

namespace MisticFy.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            AddContextServices(services, config);
            AddRepositories(services);
            AddServices(services);
        }

        private static void AddContextServices(IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("MySqlConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IProfileValidators, ProfileValidator>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISpotifyTokenRefresher, SpotifyTokenRefresher>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
