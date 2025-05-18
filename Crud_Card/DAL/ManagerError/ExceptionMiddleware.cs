namespace Crud_Card.DAL.ManagerError
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Net;
    using System.Text.Json;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Continúa con la siguiente parte del pipeline
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex , $"Ocurrió una excepción: {ex.Message}");
                await HandleExceptionAsync(httpContext , ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context , Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ( int ) HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode ,
                Message = "Ocurrió un error inesperado. Intenta nuevamente más tarde." ,
                Error = exception.Message 
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response , options));
        }
    }

}
