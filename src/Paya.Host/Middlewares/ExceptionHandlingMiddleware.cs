using Paya.Host.Exceptions;
using Paya.Host.Shared.Responses;

namespace Paya.Host.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var error = new ErrorResponse { Message = ex.Message, Code = ex.Code };
                await context.Response.WriteAsJsonAsync(error);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var error = new ErrorResponse
                {
                    Message = "خطای غیرمنتظره‌ای رخ داده است",
                    Code = "InternalServerError"
                };
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }

}
