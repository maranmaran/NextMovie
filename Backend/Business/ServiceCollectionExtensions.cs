using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITMDBMovieService, TMDBMovieService>();
            services.AddTransient<IMovieService, MovieService>();

            var tmdbSettings = new TMDBSettings();
            configuration.Bind("TMDBSettings", tmdbSettings);
            services.AddSingleton(tmdbSettings);

        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(c => { c.AddProfile<Mappings>(); });

                return config.CreateMapper();
            });
        }

    }
}
