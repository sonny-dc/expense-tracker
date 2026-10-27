using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Infrastructure.Errors;

/// <summary>
/// Global exception handler that catches unhandled exceptions and returns a standardized error response.
/// It implements the IExceptionHandler interface, which defines a method for handling exceptions.
/// The handler checks if the exception is a known ApiException and creates a ProblemDetails response accordingly.
/// If the exception is not known, it creates a generic ProblemDetails response for unexpected errors.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{

    // Category Name / Class Name in ILogger<out TCategoryName> is used 
    // to create a logger for this class. This is a common practice 
    // in .NET applications to provide context for log messages 
    // on which class they originate from when they are logged.
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

            _logger.LogWarning(
                exception,
                "A known error occurred while processing " +
                "{Method} {Path}. Trace Identifier {TraceId}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                httpContext.TraceIdentifier);
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

        return true; // returns true to indicate that the exception has been handled and no further processing is needed.
    }

    private static ProblemDetails CreateKnownProblemDetails(
        HttpContext httpContext,
        ApiException apiException)
    {
        var problemDetails = new ProblemDetails
        {
            Status = apiException.StatusCode,
            Title = apiException.Title,
            Detail = apiException.Detail,
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
