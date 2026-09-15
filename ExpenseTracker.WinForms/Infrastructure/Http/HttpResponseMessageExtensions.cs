using System.Net;
using System.Text.Json;

namespace ExpenseTracker.WinForms.Infrastructure.Http;

public static class HttpResponseMessageExtensions
{
    public static async Task EnsureApiSuccessAsync(
        this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        string responseContent =
            await response.Content.ReadAsStringAsync(
                cancellationToken);

        ApiErrorResponse? apiErrorResponse =
            TryDeserializeApiErrorResponse(responseContent);

        IReadOnlyDictionary<string, string[]> validationErrors =
            apiErrorResponse?.Errors 
            ?? TryDeserializeValidationErrors(responseContent)
            ?? new Dictionary<string, string[]>();

        string title = apiErrorResponse?.Title
            ?? GetDefaultTitle(response.StatusCode);

        string detail = apiErrorResponse?.Detail
            ?? GetDefaultDetail(response.StatusCode, validationErrors);

        throw new ApiClientException(
            statusCode: response.StatusCode,
            title: title,
            detail: detail,
            traceId: apiErrorResponse?.TraceId,
            validationErrors: validationErrors);
    }

    private static ApiErrorResponse? TryDeserializeApiErrorResponse(
        string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<ApiErrorResponse>(
                responseContent,
                JsonSerializerOptions.Web);
        } catch (JsonException)
        {
            return null;
        }
    }

    private static Dictionary<string, string[]>? TryDeserializeValidationErrors(
        string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string[]>>(
                responseContent,
                JsonSerializerOptions.Web);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string GetDefaultTitle(
        HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => "Bad Request",
            HttpStatusCode.NotFound => "Resource Not Found",
            HttpStatusCode.Conflict => "Resource Conflict",
            HttpStatusCode.InternalServerError => "Internal Server Error",
            _ => "API request failed"
        };
    }

    private static string GetDefaultDetail(
        HttpStatusCode statusCode,
        IReadOnlyDictionary<string, string[]> validationErrors)
    {
        if (validationErrors.Count > 0)
        {
            return "One or more validation errors occurred.";
        }

        return statusCode switch
        {
            HttpStatusCode.BadRequest => "The API rejected the submitted data.",
            HttpStatusCode.NotFound => "The requested resource was not found.",
            HttpStatusCode.Conflict => "The request conflicts with existing data.",
            HttpStatusCode.InternalServerError => "The server encountered an unexpected error.",
            _ => "The API request could not be completed."
        };
    }
}