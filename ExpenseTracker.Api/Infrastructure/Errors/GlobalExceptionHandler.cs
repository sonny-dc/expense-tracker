using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Infrastructure.Errors;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails;

        if (exception is ApiException apiException)
        {
            problemDetails = CreateKnownProblemDetails(
                httpContext,
                apiException);
        }
        else
        {
            _logger.LogError(
                exception,
                "An unexpected error occurred while processing " +
                "{Method} {Path}. Trace Identifier {TraceId}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.TraceIdentifier);

            problemDetails = CreateUnexpectedProblemDetails(
                httpContext);
        }

        httpContext.Response.StatusCode = 
            problemDetails.Status 
            ?? StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }

    private static ProblemDetails CreateKnownProblemDetails(
        HttpContext httpContext,
        ApiException apiException)
    {
        var problemDetails = new ProblemDetails
        {
            Status = apiException.StatusCode,
            Title = apiException.Title,
            Detail = apiException.Message,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions["traceId"] = 
            httpContext.TraceIdentifier;

        return problemDetails;
    }

    private static ProblemDetails CreateUnexpectedProblemDetails(
        HttpContext httpContext)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred while processing the request.",
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions["traceId"] = 
            httpContext.TraceIdentifier;

        return problemDetails;
    }
}
