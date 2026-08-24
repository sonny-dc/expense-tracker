using System.Net;

namespace ExpenseTracker.WinForms.Infrastructure.Http;

public sealed class ApiClientException : Exception
{
    public ApiClientException(
        HttpStatusCode statusCode,
        string title,
        string detail,
        string? traceId = null,
        IReadOnlyDictionary<string, string[]>? validationErrors = null)
        : base(detail)
    {
        StatusCode = statusCode;
        Title = title;
        Detail = detail;
        TraceId = traceId;
        ValidationErrors = validationErrors ?? new Dictionary<string, string[]>();
    }

    public HttpStatusCode StatusCode { get; }
    public string Title { get; }
    public string Detail { get; }
    public string? TraceId { get; }
    public IReadOnlyDictionary<string, string[]> ValidationErrors { get; }
    public bool HasValidationErrors => ValidationErrors.Count > 0;
    
}