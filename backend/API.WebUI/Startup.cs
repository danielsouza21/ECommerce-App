using API.Infrastructure.Data.Config;
using API.Services.Extensions;
using API.WebUI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.WebUI
{
    public class Startup
    {
        private const string CORS_POLICY_NAME = "CorsPolicy";
        private const string ORIGINS_CORS_POLICY = "https://localhost:4200";
        private const string ERROR_CONTROLLER_ENDPOINT = "/error/{0}";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            EntityFrameworkConfig.AddConfigurationContext(services, _configuration);

            services.ConfigureApplicationServices();
            services.ConfigureSwaggerServices();

            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY_NAME, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(ORIGINS_CORS_POLICY);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute(ERROR_CONTROLLER_ENDPOINT);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseCors(CORS_POLICY_NAME);

            app.UseAuthorization();

            app.ConfigureSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
