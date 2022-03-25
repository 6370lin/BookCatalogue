using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<IBookCatalogViewModelService, BookCatalogueViewModelService>();
            services.AddScoped<ISubscriptionViewModelService, SubscriptionViewModelService>();

            return services;
        }
    }
}
