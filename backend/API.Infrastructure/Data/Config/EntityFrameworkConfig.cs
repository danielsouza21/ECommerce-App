using API.Infrastructure.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure.Data.Config
{
    public static class EntityFrameworkConfig
    {
        private const string STRING_CONNECTION_STORE_DB = "StoreDb";

        public static void AddConfigurationContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(STRING_CONNECTION_STORE_DB);
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        }
    }
}
