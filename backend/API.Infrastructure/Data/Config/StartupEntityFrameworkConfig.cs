using API.Infrastructure.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure.Data.Config
{
    public static class StartupEntityFrameworkConfig
    {
        private const string STRING_CONNECTION_STORE_DB = "StoreDb";

        public static void AddEntityFrameworkConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(STRING_CONNECTION_STORE_DB);
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
        }
    }
}
