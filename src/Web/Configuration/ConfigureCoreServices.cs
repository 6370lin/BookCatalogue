using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSingleton<IUriComposer, UriComposer>();

            return services;
        }
    }
}
