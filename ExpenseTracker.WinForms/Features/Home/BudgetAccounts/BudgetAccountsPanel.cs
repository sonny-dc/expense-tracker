using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Home.BudgetAccounts;

public partial class BudgetAccountsPanel : UserControl
{
    private const int MinimumReadableCardWidth = 180;

    private readonly DisplayFormatter _displayFormatter;

    public BudgetAccountsPanel(
        DisplayFormatter displayFormatter)
    {
        InitializeComponent();

        _displayFormatter = displayFormatter;

        budgetAccountsFlowLayoutPanel.ClientSizeChanged +=
            budgetAccountsFlowLayoutPanel_ClientSizeChanged;
    }

    public void RefreshFormatting()
    {
        _ = _displayFormatter.GetCurrentPreset();
    }

    protected override void OnLoad(
        EventArgs e)
    {
        base.OnLoad(e);

        ScheduleBudgetCardWidthUpdate();
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
}
