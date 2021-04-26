using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.WebUI.ErrorHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.WebUI.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;
        private IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                var json = HandleExceptionAsync(context, ex);
                await context.Response.WriteAsync(json);
            }
        }

        #region Private Methods

        private string HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiException response;

            if (_env.IsDevelopment())
            {
                response = new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
            }
            else
            {
                response = new ApiException((int)HttpStatusCode.InternalServerError);
            }

            var optionsSerializer = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, optionsSerializer);

            return json;
        }

        #endregion
    }
}
