
namespace ExpenseTracker.WinForms.Infrastructure.Http;

internal sealed class ApiErrorResponse
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int? Status { get; set; }
    public string? TraceId { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }

}
