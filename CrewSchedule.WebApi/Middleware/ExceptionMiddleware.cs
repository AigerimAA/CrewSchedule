using CrewSchedule.Application.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace CrewSchedule.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = ex.Message
                });
            }

            catch (ForbiddenAccessException ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = ex.Message
                });
            }

            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    Errors = ex.Errors.Select(e => e.ErrorMessage)
                });
            }

            catch (InvalidOperationException ex)
            {
                //Доменные бизнес-ошибки 
                context.Response.StatusCode = 442;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = ex.Message
                });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Internal server error"
                });
            }
        }
    }
}
