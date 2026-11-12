using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PSCleanArchitectureGC.Application.Exceptions;

namespace PSCleanArchitectureGC.Api
{
    public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, problem) = exception switch
            {
                ValidationException validationException => (
                    StatusCodes.Status400BadRequest,
                    new ValidationProblemDetails(validationException.Errors)
                    {
                        Title = "One or more validation errors occurred.",
                        Status = StatusCodes.Status400BadRequest
                    }
                ),
                BadRequestException badRequestException => (
                    StatusCodes.Status400BadRequest,
                    new ProblemDetails
                    {
                        Title = "The request is invalid.",
                        Detail = badRequestException.Message,
                        Status = StatusCodes.Status400BadRequest
                    }
                ),
                NotFoundException => (
                    StatusCodes.Status404NotFound,
                    new ProblemDetails
                    {
                        Title = "The requested resource was not found.",
                        Status = StatusCodes.Status404NotFound
                    }
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    new ProblemDetails
                    {
                        Title = "An unexpected error occurred.",
                        Status = StatusCodes.Status500InternalServerError
                    }
                )
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception, "An unhandled exception occurred while processing the request.");
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                    problem,
                    problem.GetType(),
                    cancellationToken
                );

            return true;
        }
    }
}
