using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddTransient<>()
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
