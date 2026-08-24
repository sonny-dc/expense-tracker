using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ExpenseTracker.WinForms.Features.Items.Api;
using ExpenseTracker.WinForms.Features.Expenses.Api;

using ExpenseTracker.WinForms.Features.Expenses.Views;
using ExpenseTracker.WinForms.Features.Home.Views;
using ExpenseTracker.WinForms.Features.Items.Views;

using ExpenseTracker.WinForms.Shell;
using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        HostApplicationBuilder builder =
            Host.CreateApplicationBuilder();

        string apiBaseAddress =
            builder.Configuration["Api:BaseAddress"]
            ?? throw new InvalidOperationException(
                "Configuration value 'Api:BaseAddress' was not found.");

        if (!Uri.TryCreate(
                apiBaseAddress,
                UriKind.Absolute,
                out Uri? apiBaseUri))
        {
            throw new InvalidOperationException(
                "Configuration value 'Api:BaseAddress' must be a valid absolute URI.");
        }

        builder.Services.AddApiHttpClient(apiBaseUri);

        builder.Services.AddSingleton<ItemsApiClient>();
        builder.Services.AddSingleton<ExpensesApiClient>();

        builder.Services.AddSingleton<HomeView>();
        builder.Services.AddSingleton<ItemsView>();
        builder.Services.AddSingleton<ExpensesView>();

        builder.Services.AddSingleton<MainForm>();

        using IHost host = builder.Build();

        host.StartAsync()
            .GetAwaiter()
            .GetResult();

        try
        {
            MainForm mainForm =
                host.Services.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }
        finally
        {
            host.StopAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}
