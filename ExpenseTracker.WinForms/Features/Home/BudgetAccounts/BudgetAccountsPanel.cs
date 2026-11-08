using ExpenseTracker.WinForms.Features.Budget.Accounts.Models;

using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Home.BudgetAccounts;

public partial class BudgetAccountsPanel : UserControl
{
    private const int MinimumReadableCardWidth = 180;

    private readonly DisplayFormatter _displayFormatter;

    private IReadOnlyList<BudgetAccount> _budgetAccounts = [];

    public BudgetAccountsPanel(
        DisplayFormatter displayFormatter)
    {
        InitializeComponent();

        _displayFormatter = displayFormatter;

        budgetAccountsFlowLayoutPanel.ClientSizeChanged +=
            budgetAccountsFlowLayoutPanel_ClientSizeChanged;
    }

    public void DisplayAccounts(
        IReadOnlyList<BudgetAccount> budgetAccounts)
    {
        ArgumentNullException.ThrowIfNull(
            budgetAccounts);

        _budgetAccounts = budgetAccounts;

        RenderBudgetAccounts();
    }

    public void DisplayUnavailable()
    {
        _budgetAccounts = [];

        DisplayMessage(
            title:
                "Budget accounts unavailable",
            description:
                "Budget account information could not be loaded.");
    }

    public void RefreshFormatting()
    {
        RenderBudgetAccounts();
    }

    protected override void OnLoad(
        EventArgs e)
    {
        base.OnLoad(e);

        ScheduleBudgetCardWidthUpdate();
    }

    private void RenderBudgetAccounts()
    {
        budgetAccountsFlowLayoutPanel.SuspendLayout();

        try
        {
            ClearBudgetAccountCards();

            if (_budgetAccounts.Count == 0)
            {
                ConfigureEmptyState();

                budgetAccountsFlowLayoutPanel.Controls.Add(
                    emptyBudgetPanel);
            }
            else
            {
                foreach (BudgetAccount budgetAccount
                    in _budgetAccounts)
                {
                    budgetAccountsFlowLayoutPanel.Controls.Add(
                        CreateBudgetAccountCard(
                            budgetAccount));
                }
            }
        }
        finally
        {
            budgetAccountsFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }

        ScheduleBudgetCardWidthUpdate();
    }

    private void ConfigureEmptyState()
    {
        emptyBudgetTitleLabel.Text =
            "No budget accounts yet";

        emptyBudgetDescriptionLabel.Text =
            "Create a budget account to track available funds " +
            "and expense deductions.";
    }

    private void DisplayMessage(
        string title,
        string description)
    {
        budgetAccountsFlowLayoutPanel.SuspendLayout();

        try
        {
            ClearBudgetAccountCards();

            emptyBudgetTitleLabel.Text =
                title;

            emptyBudgetDescriptionLabel.Text =
                description;

            budgetAccountsFlowLayoutPanel.Controls.Add(
                emptyBudgetPanel);
        }
        finally
        {
            budgetAccountsFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }

        ScheduleBudgetCardWidthUpdate();
    }

    private void ClearBudgetAccountCards()
    {
        Control[] controls =
            budgetAccountsFlowLayoutPanel.Controls
                .Cast<Control>()
                .ToArray();

        budgetAccountsFlowLayoutPanel.Controls.Clear();

        foreach (Control control in controls)
        {
            if (!ReferenceEquals(
                    control,
                    emptyBudgetPanel))
            {
                control.Dispose();
            }
        }
    }

    private Control CreateBudgetAccountCard(
        BudgetAccount budgetAccount)
    {
        var cardPanel = new Panel
        {
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Height = 162,
            Margin = new Padding(0, 0, 0, 10),
            Width = MinimumReadableCardWidth
        };

        var nameLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold),
            ForeColor =
                Color.FromArgb(32, 40, 48),
            Location = new Point(14, 11),
            Size = new Size(
                cardPanel.Width - 108,
                25),
            Text = budgetAccount.Name
        };

        var statusLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right,
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold),
            ForeColor =
                GetStatusColor(
                    budgetAccount.Status),
            Location = new Point(
                cardPanel.Width - 90,
                12),
            Size = new Size(74, 23),
            Text =
                budgetAccount.Status.ToString(),
            TextAlign =
                ContentAlignment.MiddleRight
        };

        var balanceCaptionLabel = new Label
        {
            AutoSize = true,
            Font = new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold),
            ForeColor = Color.DimGray,
            Location = new Point(14, 45),
            Text = "CURRENT BALANCE"
        };

        var balanceValueLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold),
            ForeColor =
                GetBalanceColor(
                    budgetAccount.Balance),
            Location = new Point(12, 68),
            Size = new Size(
                cardPanel.Width - 24,
                36),
            Text =
                _displayFormatter.FormatCurrency(
                    budgetAccount.Balance)
        };

        var descriptionLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            ForeColor =
                Color.FromArgb(70, 70, 70),
            Location = new Point(14, 106),
            Size = new Size(
                cardPanel.Width - 28,
                22),
            Text =
                GetDescriptionText(
                    budgetAccount.Description)
        };

        var updatedLabel = new Label
        {
            Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right,
            AutoEllipsis = true,
            Font = new Font(
                "Segoe UI",
                8F),
            ForeColor = Color.DimGray,
            Location = new Point(14, 132),
            Size = new Size(
                cardPanel.Width - 28,
                20),
            Text =
                "Updated " +
                _displayFormatter.FormatUtcDateTime(
                    budgetAccount.LastUpdatedDateTime)
        };

        cardPanel.Controls.Add(
            nameLabel);

        cardPanel.Controls.Add(
            statusLabel);

        cardPanel.Controls.Add(
            balanceCaptionLabel);

        cardPanel.Controls.Add(
            balanceValueLabel);

        cardPanel.Controls.Add(
            descriptionLabel);

        cardPanel.Controls.Add(
            updatedLabel);

        return cardPanel;
    }

    private void budgetAccountsFlowLayoutPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateBudgetAccountCardWidths();
    }

    private void ScheduleBudgetCardWidthUpdate()
    {
        if (!IsHandleCreated)
        {
            return;
        }

        BeginInvoke(
            UpdateBudgetAccountCardWidths);
    }

    private void UpdateBudgetAccountCardWidths()
    {
        budgetAccountsFlowLayoutPanel.SuspendLayout();

        try
        {
            foreach (Control budgetCard
                in budgetAccountsFlowLayoutPanel.Controls)
            {
                budgetCard.Width =
                    GetBudgetCardWidth(
                        budgetCard.Margin);
            }
        }
        finally
        {
            budgetAccountsFlowLayoutPanel.ResumeLayout(
                performLayout: true);
        }
    }

    private int GetBudgetCardWidth(
        Padding cardMargin)
    {
        int verticalScrollBarWidth =
            budgetAccountsFlowLayoutPanel.VerticalScroll.Visible
                ? SystemInformation.VerticalScrollBarWidth
                : 0;

        int availableWidth =
            budgetAccountsFlowLayoutPanel.ClientSize.Width -
            budgetAccountsFlowLayoutPanel.Padding.Horizontal -
            cardMargin.Horizontal -
            verticalScrollBarWidth -
            4;

        return Math.Max(
            availableWidth,
            MinimumReadableCardWidth);
    }

    private static string GetDescriptionText(
        string? description)
    {
        return string.IsNullOrWhiteSpace(description)
            ? "No description"
            : description.Trim();
    }

    private static Color GetBalanceColor(
        decimal balance)
    {
        return balance switch
        {
            > 0m =>
                Color.Green,

            < 0m =>
                Color.Firebrick,

            _ =>
                Color.FromArgb(32, 40, 48)
        };
    }

    private static Color GetStatusColor(
        BudgetAccountStatus status)
    {
        return status switch
        {
            BudgetAccountStatus.Open =>
                Color.Green,

            BudgetAccountStatus.Archived =>
                Color.DarkOrange,

            BudgetAccountStatus.Settled =>
                Color.DimGray,

            _ =>
                Color.FromArgb(32, 40, 48)
        };
    }
}
