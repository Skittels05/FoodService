using AuthService.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace AuthService.API.Infrastructure;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred");
        var (statusCode, title, detail, errors) = MapException(exception);

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

        if (env.IsDevelopment())
        {
            problemDetails.Extensions["exception"] = exception.GetType().Name;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
        }

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private (int StatusCode, string Title, string Detail, IDictionary<string, string[]>? Errors) MapException(Exception exception)
    {
        return exception switch
        {
            NotFoundException or NotFoundByUserException
                => (Status404NotFound, "Not Found", exception.Message, null),
            ValidationException ex
                => (Status400BadRequest, "Validation Error", "One or more validation failures have occurred.", ex.Errors),
            DbUpdateException { InnerException: PostgresException { SqlState: "23505" } }
                => (Status409Conflict, "Conflict", "A record with this unique identifier already exists.", null),
            Exception => (Status500InternalServerError, "Internal Server Error",
                 env.IsDevelopment() ? exception.Message : "An unexpected error occurred.", null)
        };
    }
}
