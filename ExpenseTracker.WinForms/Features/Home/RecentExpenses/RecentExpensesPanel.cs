using ExpenseTracker.WinForms.Features.Expenses.Models;
using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Home.RecentExpenses;

public partial class RecentExpensesPanel : UserControl
{
    private const int MinimumReadableRowWidth = 320;

    private readonly DisplayFormatter _displayFormatter;

    private IReadOnlyList<ExpenseResult> _expenses = [];

    public RecentExpensesPanel(
        DisplayFormatter displayFormatter)
    {
        InitializeComponent();

        _displayFormatter = displayFormatter;

        recentExpensesFlowLayoutPanel.ClientSizeChanged +=
            recentExpensesFlowLayoutPanel_ClientSizeChanged;
    }

    public void DisplayExpenses(
        IReadOnlyList<ExpenseResult> expenses)
    {
        ArgumentNullException.ThrowIfNull(expenses);

        _expenses = expenses;

        RenderExpenses();
    }

    public void DisplayUnavailable()
    {
        _expenses = [];

        recentExpensesFlowLayoutPanel.SuspendLayout();

        try
        {
            recentExpensesFlowLayoutPanel.Controls.Clear();

            recentExpensesFlowLayoutPanel.Controls.Add(
                CreateMessagePanel(
                    title:
                        "Recent activity unavailable",
                    description:
                        "Recent expense information could not be loaded."));
        }
        finally
        {
            recentExpensesFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }

        ScheduleExpenseRowWidthUpdate();
    }

    public void RefreshFormatting()
    {
        RenderExpenses();
    }

    protected override void OnLoad(
        EventArgs e)
    {
        base.OnLoad(e);

        ScheduleExpenseRowWidthUpdate();
    }

    private void RenderExpenses()
    {
        recentExpensesFlowLayoutPanel.SuspendLayout();

        try
        {
            recentExpensesFlowLayoutPanel.Controls.Clear();

            if (_expenses.Count == 0)
            {
                recentExpensesFlowLayoutPanel.Controls.Add(
                    CreateMessagePanel(
                        title:
                            "No expenses yet",
                        description:
                            "Recorded expenses will appear here."));
            }
            else
            {
                foreach (ExpenseResult expense
                    in _expenses)
                {
                    recentExpensesFlowLayoutPanel.Controls.Add(
                        CreateExpenseRow(expense));
                }
            }
        }
        finally
        {
            recentExpensesFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }

        ScheduleExpenseRowWidthUpdate();
    }

    private Control CreateExpenseRow(
        ExpenseResult expense)
    {
        ExpenseEntry entry =
            expense.ExpenseEntry;

        var rowPanel = new Panel
        {
            BackColor =
                Color.White,
            BorderStyle =
                BorderStyle.FixedSingle,
            Height =
                82,
            Margin =
                new Padding(0, 0, 0, 8),
            Width =
                MinimumReadableRowWidth
        };

        var titleLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis =
                true,
            Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),
            ForeColor =
                Color.FromArgb(32, 40, 48),
            Location =
                new Point(14, 11),
            Size =
                new Size(
                    140,
                    25),
            Text =
                entry.Title
        };

        var totalLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            AutoEllipsis =
                true,
            Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),
            ForeColor =
                Color.Green,
            Location =
                new Point(
                    rowPanel.Width - 160,
                    11),
            Size =
                new Size(
                    144,
                    25),
            Text =
                _displayFormatter.FormatCurrency(
                    entry.TotalCost),
            TextAlign =
                ContentAlignment.MiddleRight
        };

        var detailsLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis =
                true,
            ForeColor =
                Color.DimGray,
            Location =
                new Point(14, 45),
            Size =
                new Size(
                    rowPanel.Width - 28,
                    23),
            Text =
                $"Expense #{entry.ExpenseEntryId} | " +
                $"{_displayFormatter.FormatUtcDateTime(entry.ExpenseDateTime)} | " +
                $"{GetItemCountText(expense.ExpenseItems.Count)}"
        };

        rowPanel.Controls.Add(
            titleLabel);

        rowPanel.Controls.Add(
            totalLabel);

        rowPanel.Controls.Add(
            detailsLabel);

        return rowPanel;
    }

    private Control CreateMessagePanel(
        string title,
        string description)
    {
        var messagePanel = new Panel
        {
            BackColor =
                Color.White,
            BorderStyle =
                BorderStyle.FixedSingle,
            Height =
                104,
            Margin =
                new Padding(0),
            Width =
                MinimumReadableRowWidth
        };

        var titleLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis =
                true,
            Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold),
            Location =
                new Point(16, 15),
            Size =
                new Size(
                    messagePanel.Width - 32,
                    25),
            Text =
                title
        };

        var descriptionLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis =
                true,
            ForeColor =
                Color.DimGray,
            Location =
                new Point(16, 49),
            Size =
                new Size(
                    messagePanel.Width - 32,
                    40),
            Text =
                description
        };

        messagePanel.Controls.Add(
            titleLabel);

        messagePanel.Controls.Add(
            descriptionLabel);

        return messagePanel;
    }

    private void recentExpensesFlowLayoutPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateExpenseRowWidths();
    }

    private void ScheduleExpenseRowWidthUpdate()
    {
        if (!IsHandleCreated)
        {
            return;
        }

        BeginInvoke(
            UpdateExpenseRowWidths);
    }

    private void UpdateExpenseRowWidths()
    {
        recentExpensesFlowLayoutPanel.SuspendLayout();

        try
        {
            foreach (Control row
                in recentExpensesFlowLayoutPanel.Controls)
            {
                row.Width =
                    GetExpenseRowWidth(
                        row.Margin);
            }
        }
        finally
        {
            recentExpensesFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }
    }

    private int GetExpenseRowWidth(
        Padding rowMargin)
    {
        int verticalScrollBarWidth =
            recentExpensesFlowLayoutPanel.VerticalScroll.Visible
                ? SystemInformation.VerticalScrollBarWidth
                : 0;

        int availableWidth =
            recentExpensesFlowLayoutPanel.ClientSize.Width -
            recentExpensesFlowLayoutPanel.Padding.Horizontal -
            rowMargin.Horizontal -
            verticalScrollBarWidth -
            4;

        return Math.Max(
            availableWidth,
            MinimumReadableRowWidth);
    }

    private static string GetItemCountText(
        int itemCount)
    {
        return itemCount == 1
            ? "1 item"
            : $"{itemCount} items";
    }
}
