using System.Linq;
using API.Core.Interfaces;
using API.Infrastructure.Data.EfCore;
using API.Services;
using API.WebUI.ErrorHandlers;
using API.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.WebUI.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)  //static type (this type) -> extension method
        {
            ConfigureDependencyInjection(services);
            ConfigureBehaviors(services);

            return services;
        }

        #region ConfigureMethods

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStoreServices, StoreServices>();
            services.AddAutoMapper(typeof(MappingProfiles));
        }

        private static void ConfigureBehaviors(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors = ActionContext.ModelState
                        .Where(error => error.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        #endregion
    }
}
