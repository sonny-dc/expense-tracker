using ExpenseTracker.WinForms.Features.Expenses.Models;
using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Home.ExpenseSummary;

public partial class ExpenseSummaryPanel : UserControl
{
    private const int SingleColumnBreakpoint = 300;
    private const int CompactTextBreakpoint = 220;

    private readonly DisplayFormatter _displayFormatter;

    private ExpenseEntrySummary? _summary;

    private bool _usesSingleColumnLayout;

    public ExpenseSummaryPanel(
        DisplayFormatter displayFormatter)
    {
        InitializeComponent();

        _displayFormatter = displayFormatter;

        ClientSizeChanged +=
            ExpenseSummaryPanel_ClientSizeChanged;

        UpdateSummaryLayout();
        UpdateResponsivePresentation();
        DisplayCurrentRegion();
    }

    public void DisplaySummary(
        ExpenseEntrySummary summary)
    {
        ArgumentNullException.ThrowIfNull(summary);

        _summary = summary;

        RenderSummary();
    }

    public void DisplayUnavailable()
    {
        _summary = null;

        expenseCountValueLabel.Text =
            "Unavailable";

        totalCostValueLabel.Text =
            "Unavailable";

        averageExpenseValueLabel.Text =
            "Unavailable";

        averageExpenseDescriptionLabel.Text =
            "Summary information could not be loaded.";

        DisplayCurrentRegion();
        UpdateResponsivePresentation();
    }

    public void RefreshFormatting()
    {
        RenderSummary();
    }

    private void ExpenseSummaryPanel_ClientSizeChanged(
        object? sender,
        EventArgs e)
    {
        UpdateSummaryLayout();
        UpdateResponsivePresentation();
    }

    private void UpdateSummaryLayout()
    {
        bool useSingleColumn =
            ClientSize.Width <
            SingleColumnBreakpoint;

        if (_usesSingleColumnLayout ==
            useSingleColumn)
        {
            UpdateCardControlLayout();

            return;
        }

        _usesSingleColumnLayout =
            useSingleColumn;

        summaryTableLayoutPanel.SuspendLayout();

        try
        {
            summaryTableLayoutPanel.Controls.Clear();
            summaryTableLayoutPanel.ColumnStyles.Clear();
            summaryTableLayoutPanel.RowStyles.Clear();

            if (_usesSingleColumnLayout)
            {
                ConfigureSingleColumnLayout();
            }
            else
            {
                ConfigureTwoColumnLayout();
            }
        }
        finally
        {
            summaryTableLayoutPanel.ResumeLayout(
                performLayout: true);
        }

        UpdateCardControlLayout();
    }

    private void ConfigureSingleColumnLayout()
    {
        summaryTableLayoutPanel.ColumnCount = 1;
        summaryTableLayoutPanel.RowCount = 4;

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100F));

        for (int rowIndex = 0;
            rowIndex < 4;
            rowIndex++)
        {
            summaryTableLayoutPanel.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent,
                    25F));
        }

        expenseCountPanel.Margin =
            new Padding(0, 0, 0, 4);

        totalCostPanel.Margin =
            new Padding(0, 4, 0, 4);

        averageExpensePanel.Margin =
            new Padding(0, 4, 0, 4);

        currentRegionPanel.Margin =
            new Padding(0, 4, 0, 0);

        summaryTableLayoutPanel.Controls.Add(
            expenseCountPanel,
            0,
            0);

        summaryTableLayoutPanel.Controls.Add(
            totalCostPanel,
            0,
            1);

        summaryTableLayoutPanel.Controls.Add(
            averageExpensePanel,
            0,
            2);

        summaryTableLayoutPanel.Controls.Add(
            currentRegionPanel,
            0,
            3);
    }

    private void ConfigureTwoColumnLayout()
    {
        summaryTableLayoutPanel.ColumnCount = 2;
        summaryTableLayoutPanel.RowCount = 2;

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                50F));

        expenseCountPanel.Margin =
            new Padding(0, 0, 7, 7);

        totalCostPanel.Margin =
            new Padding(7, 0, 0, 7);

        averageExpensePanel.Margin =
            new Padding(0, 7, 7, 0);

        currentRegionPanel.Margin =
            new Padding(7, 7, 0, 0);

        summaryTableLayoutPanel.Controls.Add(
            expenseCountPanel,
            0,
            0);

        summaryTableLayoutPanel.Controls.Add(
            totalCostPanel,
            1,
            0);

        summaryTableLayoutPanel.Controls.Add(
            averageExpensePanel,
            0,
            1);

        summaryTableLayoutPanel.Controls.Add(
            currentRegionPanel,
            1,
            1);
    }

    private void UpdateCardControlLayout()
    {
        if (_usesSingleColumnLayout)
        {
            ConfigureSingleColumnCardControls();

            return;
        }

        ConfigureTwoColumnCardControls();
    }

    private void ConfigureSingleColumnCardControls()
    {
        averageExpenseDescriptionLabel.Visible =
            false;

        currentRegionDescriptionLabel.Visible =
            false;

        ConfigureSingleColumnCard(
            expenseCountPanel,
            expenseCountCaptionLabel,
            expenseCountValueLabel);

        ConfigureSingleColumnCard(
            totalCostPanel,
            totalCostCaptionLabel,
            totalCostValueLabel);

        ConfigureSingleColumnCard(
            averageExpensePanel,
            averageExpenseCaptionLabel,
            averageExpenseValueLabel);

        ConfigureSingleColumnCard(
            currentRegionPanel,
            currentRegionCaptionLabel,
            currentRegionValueLabel);
    }

    private static void ConfigureSingleColumnCard(
        Panel cardPanel,
        Label captionLabel,
        Label valueLabel)
    {
        int cardWidth =
            Math.Max(
                cardPanel.ClientSize.Width,
                120);

        int halfWidth =
            cardWidth / 2;

        captionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left;

        captionLabel.Location =
            new Point(14, 13);

        captionLabel.Size =
            new Size(
                Math.Max(
                    halfWidth - 20,
                    80),
                24);

        captionLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        valueLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        valueLabel.Location =
            new Point(
                halfWidth,
                8);

        valueLabel.Size =
            new Size(
                Math.Max(
                    halfWidth - 16,
                    80),
                34);

        valueLabel.TextAlign =
            ContentAlignment.MiddleRight;
    }

    private void ConfigureTwoColumnCardControls()
    {
        averageExpenseDescriptionLabel.Visible =
            true;

        currentRegionDescriptionLabel.Visible =
            true;

        ConfigureStandardCard(
            expenseCountPanel,
            expenseCountCaptionLabel,
            expenseCountValueLabel);

        ConfigureStandardCard(
            totalCostPanel,
            totalCostCaptionLabel,
            totalCostValueLabel);

        ConfigureDetailedCard(
            averageExpensePanel,
            averageExpenseCaptionLabel,
            averageExpenseValueLabel,
            averageExpenseDescriptionLabel);

        ConfigureDetailedCard(
            currentRegionPanel,
            currentRegionCaptionLabel,
            currentRegionValueLabel,
            currentRegionDescriptionLabel);
    }

    private static void ConfigureStandardCard(
        Panel cardPanel,
        Label captionLabel,
        Label valueLabel)
    {
        int availableWidth =
            Math.Max(
                cardPanel.ClientSize.Width - 36,
                80);

        captionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        captionLabel.Location =
            new Point(18, 14);

        captionLabel.Size =
            new Size(
                availableWidth,
                22);

        captionLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        valueLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        valueLabel.Location =
            new Point(16, 42);

        valueLabel.Size =
            new Size(
                Math.Max(
                    cardPanel.ClientSize.Width - 32,
                    80),
                39);

        valueLabel.TextAlign =
            ContentAlignment.MiddleLeft;
    }

    private static void ConfigureDetailedCard(
        Panel cardPanel,
        Label captionLabel,
        Label valueLabel,
        Label descriptionLabel)
    {
        int availableWidth =
            Math.Max(
                cardPanel.ClientSize.Width - 36,
                80);

        captionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        captionLabel.Location =
            new Point(18, 8);

        captionLabel.Size =
            new Size(
                availableWidth,
                22);

        captionLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        valueLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        valueLabel.Location =
            new Point(16, 30);

        valueLabel.Size =
            new Size(
                Math.Max(
                    cardPanel.ClientSize.Width - 32,
                    80),
                34);

        valueLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        descriptionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        descriptionLabel.Location =
            new Point(18, 64);

        descriptionLabel.Size =
            new Size(
                availableWidth,
                25);

        descriptionLabel.TextAlign =
            ContentAlignment.MiddleLeft;
    }

    private void RenderSummary()
    {
        if (_summary is null)
        {
            DisplayCurrentRegion();
            UpdateResponsivePresentation();

            return;
        }

        totalCostValueLabel.Text =
            _displayFormatter.FormatCurrency(
                _summary.TotalCost);

        decimal averageExpense =
            _summary.ExpenseCount == 0
                ? 0m
                : Math.Round(
                    _summary.TotalCost /
                    _summary.ExpenseCount,
                    decimals: 2,
                    mode:
                        MidpointRounding.AwayFromZero);

        averageExpenseValueLabel.Text =
            _displayFormatter.FormatCurrency(
                averageExpense);

        DisplayCurrentRegion();
        UpdateResponsivePresentation();
    }

    private void UpdateResponsivePresentation()
    {
        bool useCompactExpenseText =
            _usesSingleColumnLayout ||
            expenseCountPanel.ClientSize.Width <
            CompactTextBreakpoint;

        bool useCompactTotalText =
            _usesSingleColumnLayout ||
            totalCostPanel.ClientSize.Width <
            CompactTextBreakpoint;

        bool useCompactAverageText =
            _usesSingleColumnLayout ||
            averageExpensePanel.ClientSize.Width <
            CompactTextBreakpoint;

        bool useCompactRegionText =
            _usesSingleColumnLayout ||
            currentRegionPanel.ClientSize.Width <
            CompactTextBreakpoint;

        expenseCountCaptionLabel.Text =
            useCompactExpenseText
                ? "EXPENSES"
                : "RECORDED EXPENSES";

        totalCostCaptionLabel.Text =
            useCompactTotalText
                ? "TOTAL COST"
                : "TOTAL RECORDED COST";

        averageExpenseCaptionLabel.Text =
            useCompactAverageText
                ? "AVERAGE COST"
                : "AVERAGE EXPENSE";

        currentRegionCaptionLabel.Text =
            "DISPLAY REGION";

        if (_summary is not null)
        {
            expenseCountValueLabel.Text =
                useCompactExpenseText
                    ? _summary.ExpenseCount.ToString()
                    : GetExpenseCountText(
                        _summary.ExpenseCount);

            averageExpenseDescriptionLabel.Text =
                useCompactAverageText
                    ? GetCompactAverageDescription(
                        _summary.ExpenseCount)
                    : GetAverageExpenseDescription(
                        _summary.ExpenseCount);
        }

        currentRegionDescriptionLabel.Text =
            useCompactRegionText
                ? "Money and time display."
                : "Controls displayed money and time.";

        UpdateCardControlLayout();
    }

    private void DisplayCurrentRegion()
    {
        currentRegionValueLabel.Text =
            _displayFormatter
                .GetCurrentPreset()
                .CountryName;
    }

    private static string GetExpenseCountText(
        int expenseCount)
    {
        return expenseCount == 1
            ? "1 expense"
            : $"{expenseCount} expenses";
    }

    private static string GetCompactAverageDescription(
        int expenseCount)
    {
        return expenseCount switch
        {
            0 =>
                "No expenses recorded.",

            1 =>
                "Based on 1 expense.",

            _ =>
                $"Based on {expenseCount} expenses."
        };
    }

    private static string GetAverageExpenseDescription(
        int expenseCount)
    {
        return expenseCount switch
        {
            0 =>
                "No recorded expenses are available for averaging.",

            1 =>
                "Based on 1 recorded expense.",

            _ =>
                $"Based on {expenseCount} recorded expenses."
        };
    }
}
