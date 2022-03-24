using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            if (configuration.GetConnectionString("IdentityConnection") != null)
            {
                // use real database
                // Requires LocalDB which can be installed with SQL Server Express 2016

                // Add Identity DbContext
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            }
            else
            {
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseInMemoryDatabase("Identity"));
            }
        }
    }
}