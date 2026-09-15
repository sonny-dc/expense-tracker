using System.Text.Json;

using ExpenseTracker.WinForms.Features.Expenses.Api;
using ExpenseTracker.WinForms.Features.Expenses.Models;
using ExpenseTracker.WinForms.Features.Home.BudgetAccounts;
using ExpenseTracker.WinForms.Features.Home.ExpenseSummary;
using ExpenseTracker.WinForms.Features.Home.RecentExpenses;
using ExpenseTracker.WinForms.Features.Settings.Services;

using ExpenseTracker.WinForms.Infrastructure.Dialogs;
using ExpenseTracker.WinForms.Infrastructure.Http;

namespace ExpenseTracker.WinForms.Features.Home.Views;

public partial class HomeView : UserControl
{
    private readonly ExpensesApiClient _expensesApiClient;
    private readonly DisplaySettingsService _displaySettingsService;

    private readonly ExpenseSummaryPanel _expenseSummaryPanel;
    private readonly RecentExpensesPanel _recentExpensesPanel;
    private readonly BudgetAccountsPanel _budgetAccountsPanel;

    private bool _hasLoaded;
    private bool _isLoading;

    public HomeView(
        ExpensesApiClient expensesApiClient,
        DisplaySettingsService displaySettingsService,
        ExpenseSummaryPanel expenseSummaryPanel,
        RecentExpensesPanel recentExpensesPanel,
        BudgetAccountsPanel budgetAccountsPanel)
    {
        InitializeComponent();

        _expensesApiClient = expensesApiClient;
        _displaySettingsService = displaySettingsService;

        _expenseSummaryPanel = expenseSummaryPanel;
        _recentExpensesPanel = recentExpensesPanel;
        _budgetAccountsPanel = budgetAccountsPanel;

        AddSection(
            expenseSummaryHostPanel,
            _expenseSummaryPanel);

        AddSection(
            recentExpensesHostPanel,
            _recentExpensesPanel);

        AddSection(
            budgetAccountsHostPanel,
            _budgetAccountsPanel);

        Load += HomeView_Load;

        refreshButton.Click +=
            refreshButton_Click;

        _displaySettingsService.SettingsChanged +=
            displaySettingsService_SettingsChanged;
    }

    private async void HomeView_Load(
        object? sender,
        EventArgs e)
    {
        if (_hasLoaded)
        {
            return;
        }

        _hasLoaded = true;

        await LoadDashboardAsync();
    }

    private async void refreshButton_Click(
        object? sender,
        EventArgs e)
    {
        await LoadDashboardAsync();
    }

    private async Task LoadDashboardAsync()
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            SetLoadingState(isLoading: true);

            ExpenseEntrySummary expenseSummary =
                await _expensesApiClient.GetSummaryAsync();

            IReadOnlyList<ExpenseResult> expenses =
                await _expensesApiClient.GetAllAsync();

            IReadOnlyList<ExpenseResult> recentExpenses =
                expenses
                    .Take(5)
                    .ToList();

            _expenseSummaryPanel.DisplaySummary(
                expenseSummary);

            _recentExpensesPanel.DisplayExpenses(
                recentExpenses);

            statusLabel.Text =
                "Dashboard is up to date.";
        }
        catch (ApiClientException exception)
        {
            DisplayUnavailable();

            statusLabel.Text =
                exception.Title;

            ApiErrorDialog.Show(
                this,
                exception);
        }
        catch (HttpRequestException)
        {
            DisplayUnavailable();

            statusLabel.Text =
                "Unable to connect to the ExpenseTracker service.";

            MessageBox.Show(
                this,
                "The dashboard could not be loaded. " +
                "Make sure the ExpenseTracker service is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            DisplayUnavailable();

            statusLabel.Text =
                "The dashboard returned an unexpected response.";

            MessageBox.Show(
                this,
                "The dashboard information could not be read. " +
                "The application components may not be using matching data contracts.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            DisplayUnavailable();

            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "Loading the dashboard took too long. Please try again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private void DisplayUnavailable()
    {
        _expenseSummaryPanel.DisplayUnavailable();
        _recentExpensesPanel.DisplayUnavailable();
    }

    private void displaySettingsService_SettingsChanged(
        object? sender,
        EventArgs e)
    {
        _expenseSummaryPanel.RefreshFormatting();
        _recentExpensesPanel.RefreshFormatting();
        _budgetAccountsPanel.RefreshFormatting();
    }

    private void SetLoadingState(
        bool isLoading)
    {
        _isLoading = isLoading;

        UseWaitCursor = isLoading;

        refreshButton.Enabled =
            !isLoading;

        dashboardTableLayoutPanel.Enabled =
            !isLoading;

        if (isLoading)
        {
            statusLabel.Text =
                "Loading dashboard...";
        }
    }

    private static void AddSection(
        Panel hostPanel,
        UserControl section)
    {
        section.Dock =
            DockStyle.Fill;

        hostPanel.Controls.Add(
            section);
    }
}
