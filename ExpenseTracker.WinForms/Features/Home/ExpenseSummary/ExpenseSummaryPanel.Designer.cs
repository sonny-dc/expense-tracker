namespace ExpenseTracker.WinForms.Features.Home.ExpenseSummary;

partial class ExpenseSummaryPanel
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(
        bool disposing)
    {
        if (disposing &&
            components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        summaryTableLayoutPanel =
            new TableLayoutPanel();

        expenseCountPanel =
            new Panel();

        expenseCountCaptionLabel =
            new Label();

        expenseCountValueLabel =
            new Label();

        totalCostPanel =
            new Panel();

        totalCostCaptionLabel =
            new Label();

        totalCostValueLabel =
            new Label();

        averageExpensePanel =
            new Panel();

        averageExpenseCaptionLabel =
            new Label();

        averageExpenseValueLabel =
            new Label();

        averageExpenseDescriptionLabel =
            new Label();

        currentRegionPanel =
            new Panel();

        currentRegionCaptionLabel =
            new Label();

        currentRegionValueLabel =
            new Label();

        currentRegionDescriptionLabel =
            new Label();

        summaryTableLayoutPanel.SuspendLayout();
        expenseCountPanel.SuspendLayout();
        totalCostPanel.SuspendLayout();
        averageExpensePanel.SuspendLayout();
        currentRegionPanel.SuspendLayout();
        SuspendLayout();

        //
        // summaryTableLayoutPanel
        //
        summaryTableLayoutPanel.ColumnCount = 2;

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                50F));

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

        summaryTableLayoutPanel.Dock =
            DockStyle.Fill;

        summaryTableLayoutPanel.Location =
            new Point(0, 0);

        summaryTableLayoutPanel.Margin =
            new Padding(0);

        summaryTableLayoutPanel.Name =
            "summaryTableLayoutPanel";

        summaryTableLayoutPanel.RowCount = 2;

        summaryTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                50F));

        summaryTableLayoutPanel.Size =
            new Size(480, 212);

        summaryTableLayoutPanel.TabIndex = 0;

        //
        // expenseCountPanel
        //
        ConfigureCardPanel(
            expenseCountPanel);

        expenseCountPanel.Controls.Add(
            expenseCountValueLabel);

        expenseCountPanel.Controls.Add(
            expenseCountCaptionLabel);

        expenseCountPanel.Margin =
            new Padding(0, 0, 7, 7);

        //
        // expenseCountCaptionLabel
        //
        ConfigureCaptionLabel(
            expenseCountCaptionLabel);

        expenseCountCaptionLabel.Text =
            "EXPENSES";

        //
        // expenseCountValueLabel
        //
        ConfigureValueLabel(
            expenseCountValueLabel);

        expenseCountValueLabel.Font =
            new Font(
                "Segoe UI",
                14F,
                FontStyle.Bold);

        expenseCountValueLabel.Text =
            "0 expenses";

        //
        // totalCostPanel
        //
        ConfigureCardPanel(
            totalCostPanel);

        totalCostPanel.Controls.Add(
            totalCostValueLabel);

        totalCostPanel.Controls.Add(
            totalCostCaptionLabel);

        totalCostPanel.Margin =
            new Padding(7, 0, 0, 7);

        //
        // totalCostCaptionLabel
        //
        ConfigureCaptionLabel(
            totalCostCaptionLabel);

        totalCostCaptionLabel.Text =
            "TOTAL COST";

        //
        // totalCostValueLabel
        //
        ConfigureValueLabel(
            totalCostValueLabel);

        totalCostValueLabel.Font =
            new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold);

        totalCostValueLabel.ForeColor =
            Color.Green;

        totalCostValueLabel.Text =
            "₱0.00";

        //
        // averageExpensePanel
        //
        ConfigureCardPanel(
            averageExpensePanel);

        averageExpensePanel.BackColor =
            Color.FromArgb(247, 250, 248);

        averageExpensePanel.Controls.Add(
            averageExpenseDescriptionLabel);

        averageExpensePanel.Controls.Add(
            averageExpenseValueLabel);

        averageExpensePanel.Controls.Add(
            averageExpenseCaptionLabel);

        averageExpensePanel.Margin =
            new Padding(0, 7, 7, 0);

        //
        // averageExpenseCaptionLabel
        //
        ConfigureCaptionLabel(
            averageExpenseCaptionLabel);

        averageExpenseCaptionLabel.Text =
            "AVERAGE COST";

        //
        // averageExpenseValueLabel
        //
        ConfigureValueLabel(
            averageExpenseValueLabel);

        averageExpenseValueLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);

        averageExpenseValueLabel.ForeColor =
            Color.Green;

        averageExpenseValueLabel.Text =
            "₱0.00";

        //
        // averageExpenseDescriptionLabel
        //
        ConfigureDescriptionLabel(
            averageExpenseDescriptionLabel);

        averageExpenseDescriptionLabel.Text =
            "Based on recorded expenses.";

        //
        // currentRegionPanel
        //
        ConfigureCardPanel(
            currentRegionPanel);

        currentRegionPanel.BackColor =
            Color.FromArgb(247, 250, 248);

        currentRegionPanel.Controls.Add(
            currentRegionDescriptionLabel);

        currentRegionPanel.Controls.Add(
            currentRegionValueLabel);

        currentRegionPanel.Controls.Add(
            currentRegionCaptionLabel);

        currentRegionPanel.Margin =
            new Padding(7, 7, 0, 0);

        //
        // currentRegionCaptionLabel
        //
        ConfigureCaptionLabel(
            currentRegionCaptionLabel);

        currentRegionCaptionLabel.Text =
            "DISPLAY REGION";

        //
        // currentRegionValueLabel
        //
        ConfigureValueLabel(
            currentRegionValueLabel);

        currentRegionValueLabel.Font =
            new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

        currentRegionValueLabel.Text =
            "Philippines";

        //
        // currentRegionDescriptionLabel
        //
        ConfigureDescriptionLabel(
            currentRegionDescriptionLabel);

        currentRegionDescriptionLabel.Text =
            "Controls displayed money and time.";

        //
        // ExpenseSummaryPanel
        //
        AutoScaleDimensions =
            new SizeF(8F, 20F);

        AutoScaleMode =
            AutoScaleMode.Font;

        BackColor =
            Color.FromArgb(245, 247, 250);

        Controls.Add(
            summaryTableLayoutPanel);

        Name =
            "ExpenseSummaryPanel";

        Size =
            new Size(480, 212);

        summaryTableLayoutPanel.ResumeLayout(false);
        expenseCountPanel.ResumeLayout(false);
        totalCostPanel.ResumeLayout(false);
        averageExpensePanel.ResumeLayout(false);
        averageExpensePanel.PerformLayout();
        currentRegionPanel.ResumeLayout(false);
        currentRegionPanel.PerformLayout();
        ResumeLayout(false);
    }

    private static void ConfigureCardPanel(
        Panel panel)
    {
        panel.BackColor =
            Color.White;

        panel.BorderStyle =
            BorderStyle.FixedSingle;

        panel.Dock =
            DockStyle.Fill;

        panel.Name =
            $"{panel.Name}";

        panel.TabIndex = 0;
    }

    private static void ConfigureCaptionLabel(
        Label label)
    {
        label.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        label.AutoEllipsis =
            true;

        label.Font =
            new Font(
                "Segoe UI",
                8F,
                FontStyle.Bold);

        label.ForeColor =
            Color.DimGray;

        label.Location =
            new Point(16, 13);

        label.Size =
            new Size(195, 21);

        label.TabIndex = 0;
    }

    private static void ConfigureValueLabel(
        Label label)
    {
        label.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        label.AutoEllipsis =
            true;

        label.ForeColor =
            Color.FromArgb(32, 40, 48);

        label.Location =
            new Point(14, 42);

        label.Size =
            new Size(199, 35);

        label.TabIndex = 1;
    }

    private static void ConfigureDescriptionLabel(
        Label label)
    {
        label.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        label.AutoEllipsis =
            true;

        label.Font =
            new Font(
                "Segoe UI",
                8F);

        label.ForeColor =
            Color.DimGray;

        label.Location =
            new Point(16, 76);

        label.Size =
            new Size(195, 21);

        label.TabIndex = 2;
    }

    #endregion

    private TableLayoutPanel summaryTableLayoutPanel;

    private Panel expenseCountPanel;
    private Label expenseCountCaptionLabel;
    private Label expenseCountValueLabel;

    private Panel totalCostPanel;
    private Label totalCostCaptionLabel;
    private Label totalCostValueLabel;

    private Panel averageExpensePanel;
    private Label averageExpenseCaptionLabel;
    private Label averageExpenseValueLabel;
    private Label averageExpenseDescriptionLabel;

    private Panel currentRegionPanel;
    private Label currentRegionCaptionLabel;
    private Label currentRegionValueLabel;
    private Label currentRegionDescriptionLabel;
}
