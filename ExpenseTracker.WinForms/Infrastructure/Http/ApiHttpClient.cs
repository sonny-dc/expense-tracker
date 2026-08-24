using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.WinForms.Infrastructure.Http;

public static class ApiHttpClient
{
    private const string Name = "ExpenseTrackerApi";

    public static IServiceCollection AddApiHttpClient(
        this IServiceCollection services,
        Uri baseAddress)
    {
        services.AddHttpClient(
            Name, 
            httpClient =>
            {
                httpClient.BaseAddress = baseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

    public static HttpClient CreateApiClient(
        this IHttpClientFactory httpClientFactory)
    {
        return httpClientFactory.CreateClient(Name);
    }

}
