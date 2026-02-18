using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using AuthService.Application.Exceptions;

namespace AuthService.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing Request: {Name} {@Request}", requestName, request);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            var response = await next();

            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 500)
            {
                logger.LogWarning("Long Running Request: {Name} ({Elapsed} ms)", requestName, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                logger.LogInformation("Completed Request: {Name} ({Elapsed} ms)", requestName, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            if (ex is ValidationException)
            {
                logger.LogWarning("Validation failed for {Name}. Error: {Message}", requestName, ex.Message);
            }
            else
            {
                logger.LogError(ex, "Request Failure: {Name} {@Request} ({Elapsed} ms)", requestName, request, stopwatch.ElapsedMilliseconds);
            }
            throw;
        }
    }
}
