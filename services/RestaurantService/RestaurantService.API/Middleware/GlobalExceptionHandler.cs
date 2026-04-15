using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestaurantService.BLL.Exceptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace RestaurantService.API.Middleware;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail, errors) = MapException(exception);

        if (statusCode >= 500)
        {
            logger.LogError(exception, "An unhandled exception occurred processing request {Path}", httpContext.Request.Path);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        if (errors != null)
        {
            problemDetails.Extensions["errors"] = errors;
        }
        
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private (int StatusCode, string Title, string Detail, IDictionary<string, string[]>? Errors) MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException => (Status404NotFound, "Not Found", exception.Message, null),
            
            DbUpdateException { InnerException: PostgresException { SqlState: "23505" } }
                => (Status409Conflict, "Conflict", "A record with this unique identifier already exists.", null),

            BusinessRuleException
                => (Status400BadRequest, "Business Rule Violation", exception.Message, null),
            
            FluentValidation.ValidationException ex
                => (Status400BadRequest, "Validation Error", "One or more validation failures have occurred.", 
                    ex.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())),
            
            Exception => (Status500InternalServerError, "Internal Server Error",
                 env.IsDevelopment() ? exception.Message : "An unexpected error occurred.", null)
        };
    }
}
