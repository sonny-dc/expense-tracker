namespace ExpenseTracker.Api.Infrastructure.Errors;

public abstract class ApiException : Exception
{
    protected ApiException(
        int statusCode,
        string title,
        string detail)
        : base(detail)
    {
        StatusCode = statusCode;
        Title = title;
    }

    public int StatusCode { get; }

    public string Title { get; }
}
