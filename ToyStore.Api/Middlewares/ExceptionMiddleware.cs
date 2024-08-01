using System.Text.Json;
using ToyStore.Api.Errors;

namespace ToyStore.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger,IHostEnvironment environment) {
            _logger = logger;
            _next = next;   
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message);
                context.Response.ContentType= "Application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = _environment.IsDevelopment() ? new ApiResponseDetail(StatusCodes.Status500InternalServerError, e.Message, e.StackTrace)
                    : new ApiResponse(StatusCodes.Status500InternalServerError, e.Message);

                var opt = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var responseApi = JsonSerializer.Serialize(response, opt);

                await context.Response.WriteAsync(responseApi);
            
            }
        
        
        }
    }
}
