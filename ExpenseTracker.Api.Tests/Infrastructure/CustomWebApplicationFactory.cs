using Microsoft.Extensions.Logging;

using ExpenseTracker.Api.Infrastructure.Database;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTracker.Api.Tests.Infrastructure;

public sealed class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private const string TestConnectionString =
        "Server=(localdb)\\MSSQLLocalDB;" +
        "Database=ExpenseTracker.Tests;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True";

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureLogging(logging =>
        {
            // Suppress repeated logs from the hosting lifetime
            logging.AddFilter(
                "Microsoft.Hosting.Lifetime",
                LogLevel.None);

            // Suppress warnings when the test host cannot determine an HTTPS port
            logging.AddFilter(
                "Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware",
                LogLevel.None);

            // Suppress expected operational warnings while retaining unexpected
            // errors from the global exception handler for test diagnostics.
            logging.AddFilter(
                "ExpenseTracker.Api.Infrastructure.Errors.GlobalExceptionHandler",
                LogLevel.Error);
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<SqlConnectionFactory>();

            services.AddSingleton(
                new SqlConnectionFactory(TestConnectionString));
        });
    }
}
