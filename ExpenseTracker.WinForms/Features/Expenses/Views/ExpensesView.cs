using System.Text.Json;
using System.Globalization;

using ExpenseTracker.WinForms.Features.Expenses.Api;
using ExpenseTracker.WinForms.Features.Expenses.Models;
using ExpenseTracker.WinForms.Infrastructure.Dialogs;
using ExpenseTracker.WinForms.Infrastructure.Http;

using ExpenseTracker.WinForms.Features.Items.Models;

using ExpenseTracker.WinForms.Features.Items.Api;

namespace ExpenseTracker.WinForms.Features.Expenses.Views;

public partial class ExpensesView : UserControl
{
    private readonly ExpensesApiClient _expensesApiClient;
    private readonly ItemsApiClient _itemsApiClient;

    private IReadOnlyList<ExpenseResult> _expenses = [];

    private bool _hasLoaded;
    private bool _isLoading;

    public ExpensesView(
        ExpensesApiClient expensesApiClient,
        ItemsApiClient itemsApiClient)
    {
        InitializeComponent();

        _expensesApiClient = expensesApiClient;
        _itemsApiClient = itemsApiClient;

        Load += ExpensesView_Load;
        refreshButton.Click += refreshButton_Click;
        recordExpenseButton.Click += recordExpenseButton_Click;

        expensesFlowLayoutPanel.ClientSizeChanged +=
            expensesFlowLayoutPanel_ClientSizeChanged;
    }

    private async void ExpensesView_Load(
        object? sender,
        EventArgs e)
    {
        if (_hasLoaded)
        {
            return;
        }

        _hasLoaded = true;

        await LoadExpensesAsync();
    }

    private async void recordExpenseButton_Click(
        object? sender,
        EventArgs e)
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            SetLoadingState(isLoading: true);

            statusLabel.Text =
                "Loading available items...";

            IReadOnlyList<Item> items =
                await _itemsApiClient.GetAllAsync();

            if (items.Count == 0)
            {
                statusLabel.Text =
                    "No items are available.";

                MessageBox.Show(
                    this,
                    "An expense cannot be recorded until at least one item exists. " +
                    "Add an item from the Items page, then try again.",
                    "No Items Available",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var recordExpenseForm =
                new RecordExpenseForm(items);

            DialogResult dialogResult =
                recordExpenseForm.ShowDialog(this);

            if (dialogResult != DialogResult.OK ||
                recordExpenseForm.ExpenseRequest is null)
            {
                UpdateStatus();

                return;
            }

            await CreateExpenseAsync(
                recordExpenseForm.ExpenseRequest);
        }
        catch (ApiClientException exception)
        {
            statusLabel.Text = exception.Title;

            ApiErrorDialog.Show(
                this,
                exception);
        }
        catch (HttpRequestException)
        {
            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "Available items could not be loaded because the API is unavailable. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The available item list could not be read. " +
                "The client and API contracts may not match.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "Loading the available items took too long. Please try again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private async void refreshButton_Click(
        object? sender,
        EventArgs e)
    {
        await LoadExpensesAsync();
    }

    private async Task LoadExpensesAsync()
    {
        if (_isLoading)
        {
            return;
        }

        try
        {
            SetLoadingState(isLoading: true);

            _expenses =
                await _expensesApiClient.GetAllAsync();

            DisplayExpenses();
        }
        catch (ApiClientException exception)
        {
            _expenses = [];

            expensesFlowLayoutPanel.Controls.Clear();

            statusLabel.Text = exception.Title;

            ApiErrorDialog.Show(
                this,
                exception);
        }
        catch (HttpRequestException)
        {
            _expenses = [];

            expensesFlowLayoutPanel.Controls.Clear();

            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The expense list could not be loaded. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            _expenses = [];

            expensesFlowLayoutPanel.Controls.Clear();

            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The API response could not be read. " +
                "The client and API contracts may not match.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The API request took too long to complete. " +
                "Please try again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private async Task CreateExpenseAsync(
        CreateExpenseRequest request)
    {
        try
        {
            SetLoadingState(isLoading: true);

            statusLabel.Text =
                "Recording expense...";

            ExpenseResult createdExpense =
                await _expensesApiClient.CreateAsync(
                    request);

            _expenses =
                await _expensesApiClient.GetAllAsync();

            DisplayExpenses();

            MessageBox.Show(
                this,
                $"Expense #{createdExpense.ExpenseEntry.ExpenseEntryId} " +
                $"was recorded successfully.\n\n" +
                $"Final total: {FormatPeso(createdExpense.ExpenseEntry.TotalCost)}",
                "Expense Recorded",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (ApiClientException exception)
        {
            statusLabel.Text = exception.Title;

            ApiErrorDialog.Show(
                this,
                exception);
        }
        catch (HttpRequestException)
        {
            statusLabel.Text =
                "Unable to connect to the ExpenseTracker API.";

            MessageBox.Show(
                this,
                "The expense could not be recorded because the API is unavailable. " +
                "Make sure the ExpenseTracker API is running, then try again.",
                "Connection Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (JsonException)
        {
            statusLabel.Text =
                "The API returned an unexpected response.";

            MessageBox.Show(
                this,
                "The expense may have been recorded, but the API response " +
                "could not be read. Refresh the expense list before trying again.",
                "Response Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (TaskCanceledException)
        {
            statusLabel.Text =
                "The request timed out.";

            MessageBox.Show(
                this,
                "The request took too long to complete. " +
                "Refresh the expense list before trying again.",
                "Request Timeout",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            SetLoadingState(isLoading: false);
        }
    }

    private void DisplayExpenses()
    {
        expensesFlowLayoutPanel.SuspendLayout();

        try
        {
            expensesFlowLayoutPanel.Controls.Clear();

            foreach (ExpenseResult expense in _expenses)
            {
                Control expenseCard =
                    CreateExpenseCard(expense);

                expensesFlowLayoutPanel.Controls.Add(
                    expenseCard);
            }

            UpdateExpenseCardWidths();
            UpdateStatus();
        }
        finally
        {
            expensesFlowLayoutPanel.ResumeLayout();
        }
    }

    private Control CreateExpenseCard(
        ExpenseResult expense)
    {
        ExpenseEntry entry = expense.ExpenseEntry;

        var cardPanel = new Panel
        {
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(0, 0, 0, 12),
            Padding = new Padding(18),
            Width = 740,
            Height = 176
        };

        var titleLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            AutoSize = false,
            Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold),
            ForeColor = Color.FromArgb(
                red: 32,
                green: 40,
                blue: 48),
            Location = new Point(18, 14),
            Size = new Size(
                cardPanel.Width - 250,
                32),
            Text = entry.Title
        };

        var totalLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            Font = new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold),
            ForeColor = Color.Green,
            Location = new Point(
                cardPanel.Width - 216,
                14),
            Size = new Size(196, 32),
            Text = FormatPeso(entry.TotalCost),
            TextAlign =
                ContentAlignment.MiddleRight
        };

        var numberLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.DimGray,
            Location = new Point(18, 51),
            Text =
                $"Expense #{entry.ExpenseEntryId}"
        };

        var dateLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.DimGray,
            Location = new Point(150, 51),
            Text = FormatExpenseDateTime(
                entry.ExpenseDateTime)
        };

        var itemCountLabel = new Label
        {
            AutoSize = true,
            ForeColor = Color.DimGray,
            Location = new Point(18, 82),
            Text = GetItemCountText(
                expense.ExpenseItems.Count)
        };

        var notesLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            AutoSize = false,
            ForeColor = Color.FromArgb(
                red: 70,
                green: 70,
                blue: 70),
            Location = new Point(18, 111),
            Size = new Size(
                cardPanel.Width - 174,
                42),
            Text = GetNotesPreview(entry.Notes)
        };

        var viewMoreButton = new Button
        {
            Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right,
            Cursor = Cursors.Hand,
            Location = new Point(
                cardPanel.Width - 126,
                115),
            Size = new Size(106, 38),
            Text = "View More",
            UseVisualStyleBackColor = true
        };

        viewMoreButton.Click +=
            (_, _) => ShowExpenseDetails(expense);

        cardPanel.Controls.Add(titleLabel);
        cardPanel.Controls.Add(totalLabel);
        cardPanel.Controls.Add(numberLabel);
        cardPanel.Controls.Add(dateLabel);
        cardPanel.Controls.Add(itemCountLabel);
        cardPanel.Controls.Add(notesLabel);
        cardPanel.Controls.Add(viewMoreButton);

        return cardPanel;
    }

    private void ShowExpenseDetails(
        ExpenseResult expense)
    {
        using var expenseDetailsForm =
            new ExpenseDetailsForm(expense);

        expenseDetailsForm.ShowDialog(this);
    }

    private void expensesFlowLayoutPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateExpenseCardWidths();
    }

    private void UpdateExpenseCardWidths()
    {
        int availableWidth =
            expensesFlowLayoutPanel.ClientSize.Width -
            expensesFlowLayoutPanel.Padding.Horizontal -
            SystemInformation.VerticalScrollBarWidth -
            4;

        foreach (Control expenseCard
            in expensesFlowLayoutPanel.Controls)
        {
            expenseCard.Width =
                Math.Max(
                    availableWidth,
                    300);
        }
    }

    private void SetLoadingState(
        bool isLoading)
    {
        _isLoading = isLoading;

        UseWaitCursor = isLoading;

        refreshButton.Enabled = !isLoading;
        recordExpenseButton.Enabled = !isLoading;
        expensesFlowLayoutPanel.Enabled = !isLoading;

        if (isLoading)
        {
            statusLabel.Text =
                "Loading expenses...";
        }
    }

    private void UpdateStatus()
    {
        statusLabel.Text =
            _expenses.Count switch
            {
                0 =>
                    "No expenses have been recorded.",

                1 =>
                    "1 expense",

                _ =>
                    $"{_expenses.Count} expenses"
            };
    }

    private static string FormatExpenseDateTime(
        DateTime expenseDateTime)
    {
        DateTime utcDateTime =
            expenseDateTime.Kind == DateTimeKind.Utc
                ? expenseDateTime
                : DateTime.SpecifyKind(
                    expenseDateTime,
                    DateTimeKind.Utc);

        DateTime localDateTime =
            utcDateTime.ToLocalTime();

        return localDateTime.ToString(
            "MMM d, yyyy h:mm tt");
    }

    private static string GetItemCountText(
        int itemCount)
    {
        return itemCount == 1
            ? "1 item"
            : $"{itemCount} items";
    }

    private static string GetNotesPreview(
        string? notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
        {
            return "No notes";
        }

        string normalizedNotes = string.Join(
            " ",
            notes.Split(
                ['\r', '\n'],
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries));

        const int maximumLength = 100;

        if (normalizedNotes.Length <= maximumLength)
        {
            return normalizedNotes;
        }

        return string.Concat(
            normalizedNotes.AsSpan(
                start: 0,
                length: maximumLength),
            "...");
    }

    private static string FormatPeso(
        decimal amount)
    {
        return amount.ToString(
            "C2",
            CultureInfo.GetCultureInfo("en-PH"));
    }
}
