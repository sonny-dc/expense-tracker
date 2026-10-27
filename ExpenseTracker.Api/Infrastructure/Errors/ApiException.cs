namespace ExpenseTracker.Api.Infrastructure.Errors;

public abstract class ApiException : Exception
{
    protected ApiException(
        int statusCode,
        string title,
        string detail,
        string message)
        : base(message)
    {
        StatusCode = statusCode;
        Title = title;
        Detail = detail;
    }

    public int StatusCode { get; }

    public string Title { get; }

    public string Detail { get; }
}
