using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ExpenseTracker.WinForms.Features.Expenses.Views;
using ExpenseTracker.WinForms.Features.Home.Views;
using ExpenseTracker.WinForms.Features.Items.Views;
using ExpenseTracker.WinForms.Shell;

namespace ExpenseTracker.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        HostApplicationBuilder builder =
            Host.CreateApplicationBuilder();

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
