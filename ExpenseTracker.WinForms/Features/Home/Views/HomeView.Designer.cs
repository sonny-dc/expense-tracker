namespace ExpenseTracker.WinForms.Features.Home.Views;

partial class HomeView
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
        pageLayoutPanel =
            new TableLayoutPanel();

        headingPanel =
            new Panel();

        pageTitleLabel =
            new Label();

        pageDescriptionLabel =
            new Label();

        refreshButton =
            new Button();

        dashboardTableLayoutPanel =
            new TableLayoutPanel();

        leftColumnTableLayoutPanel =
            new TableLayoutPanel();

        expenseSummaryHostPanel =
            new Panel();

        recentExpensesHostPanel =
            new Panel();

        budgetAccountsHostPanel =
            new Panel();

        statusLabel =
            new Label();

        pageLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        dashboardTableLayoutPanel.SuspendLayout();
        leftColumnTableLayoutPanel.SuspendLayout();
        SuspendLayout();

        //
        // pageLayoutPanel
        //
        pageLayoutPanel.ColumnCount = 1;

        pageLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100F));

        pageLayoutPanel.Controls.Add(
            headingPanel,
            0,
            0);

        pageLayoutPanel.Controls.Add(
            dashboardTableLayoutPanel,
            0,
            1);

        pageLayoutPanel.Controls.Add(
            statusLabel,
            0,
            2);

        pageLayoutPanel.Dock =
            DockStyle.Fill;

        pageLayoutPanel.Location =
            new Point(0, 0);

        pageLayoutPanel.Name =
            "pageLayoutPanel";

        pageLayoutPanel.Padding =
            new Padding(24);

        pageLayoutPanel.RowCount = 3;

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                82F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                36F));

        pageLayoutPanel.Size =
            new Size(842, 653);

        pageLayoutPanel.TabIndex = 0;

        //
        // headingPanel
        //
        headingPanel.Controls.Add(
            refreshButton);

        headingPanel.Controls.Add(
            pageDescriptionLabel);

        headingPanel.Controls.Add(
            pageTitleLabel);

        headingPanel.Dock =
            DockStyle.Fill;

        headingPanel.Location =
            new Point(27, 27);

        headingPanel.Name =
            "headingPanel";

        headingPanel.Size =
            new Size(788, 76);

        headingPanel.TabIndex = 0;

        //
        // pageTitleLabel
        //
        pageTitleLabel.AutoSize =
            true;

        pageTitleLabel.Font =
            new Font(
                "Segoe UI",
                20F,
                FontStyle.Bold);

        pageTitleLabel.ForeColor =
            Color.FromArgb(32, 40, 48);

        pageTitleLabel.Location =
            new Point(-3, -5);

        pageTitleLabel.Name =
            "pageTitleLabel";

        pageTitleLabel.Size =
            new Size(116, 46);

        pageTitleLabel.TabIndex = 0;

        pageTitleLabel.Text =
            "Home";

        //
        // pageDescriptionLabel
        //
        pageDescriptionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        pageDescriptionLabel.AutoEllipsis =
            true;

        pageDescriptionLabel.ForeColor =
            Color.DimGray;

        pageDescriptionLabel.Location =
            new Point(0, 43);

        pageDescriptionLabel.Name =
            "pageDescriptionLabel";

        pageDescriptionLabel.Size =
            new Size(650, 24);

        pageDescriptionLabel.TabIndex = 1;

        pageDescriptionLabel.Text =
            "Review your expense activity and available budget accounts.";

        //
        // refreshButton
        //
        refreshButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        refreshButton.Cursor =
            Cursors.Hand;

        refreshButton.Location =
            new Point(694, 12);

        refreshButton.Name =
            "refreshButton";

        refreshButton.Size =
            new Size(94, 40);

        refreshButton.TabIndex = 2;

        refreshButton.Text =
            "Refresh";

        refreshButton.UseVisualStyleBackColor =
            true;

        //
        // dashboardTableLayoutPanel
        //
        dashboardTableLayoutPanel.ColumnCount = 2;

        dashboardTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                62F));

        dashboardTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                38F));

        dashboardTableLayoutPanel.Controls.Add(
            leftColumnTableLayoutPanel,
            0,
            0);

        dashboardTableLayoutPanel.Controls.Add(
            budgetAccountsHostPanel,
            1,
            0);

        dashboardTableLayoutPanel.Dock =
            DockStyle.Fill;

        dashboardTableLayoutPanel.Location =
            new Point(27, 109);

        dashboardTableLayoutPanel.Name =
            "dashboardTableLayoutPanel";

        dashboardTableLayoutPanel.RowCount = 1;

        dashboardTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        dashboardTableLayoutPanel.Size =
            new Size(788, 481);

        dashboardTableLayoutPanel.TabIndex = 1;

        //
        // leftColumnTableLayoutPanel
        //
        leftColumnTableLayoutPanel.ColumnCount = 1;

        leftColumnTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100F));

        leftColumnTableLayoutPanel.Controls.Add(
            expenseSummaryHostPanel,
            0,
            0);

        leftColumnTableLayoutPanel.Controls.Add(
            recentExpensesHostPanel,
            0,
            1);

        leftColumnTableLayoutPanel.Dock =
            DockStyle.Fill;

        leftColumnTableLayoutPanel.Location =
            new Point(0, 0);

        leftColumnTableLayoutPanel.Margin =
            new Padding(0, 0, 8, 0);

        leftColumnTableLayoutPanel.Name =
            "leftColumnTableLayoutPanel";

        leftColumnTableLayoutPanel.RowCount = 2;

        leftColumnTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                224F));

        leftColumnTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        leftColumnTableLayoutPanel.Size =
            new Size(480, 481);

        leftColumnTableLayoutPanel.TabIndex = 0;

        //
        // expenseSummaryHostPanel
        //
        expenseSummaryHostPanel.Dock =
            DockStyle.Fill;

        expenseSummaryHostPanel.Location =
            new Point(0, 0);

        expenseSummaryHostPanel.Margin =
            new Padding(0, 0, 0, 12);

        expenseSummaryHostPanel.Name =
            "expenseSummaryHostPanel";

        expenseSummaryHostPanel.Size =
            new Size(480, 212);

        expenseSummaryHostPanel.TabIndex = 0;

        //
        // recentExpensesHostPanel
        //
        recentExpensesHostPanel.Dock =
            DockStyle.Fill;

        recentExpensesHostPanel.Location =
            new Point(0, 224);

        recentExpensesHostPanel.Margin =
            new Padding(0);

        recentExpensesHostPanel.Name =
            "recentExpensesHostPanel";

        recentExpensesHostPanel.Size =
            new Size(480, 257);

        recentExpensesHostPanel.TabIndex = 1;

        //
        // budgetAccountsHostPanel
        //
        budgetAccountsHostPanel.Dock =
            DockStyle.Fill;

        budgetAccountsHostPanel.Location =
            new Point(496, 0);

        budgetAccountsHostPanel.Margin =
            new Padding(8, 0, 0, 0);

        budgetAccountsHostPanel.Name =
            "budgetAccountsHostPanel";

        budgetAccountsHostPanel.Size =
            new Size(292, 481);

        budgetAccountsHostPanel.TabIndex = 1;

        //
        // statusLabel
        //
        statusLabel.Dock =
            DockStyle.Fill;

        statusLabel.ForeColor =
            Color.DimGray;

        statusLabel.Location =
            new Point(27, 593);

        statusLabel.Name =
            "statusLabel";

        statusLabel.Size =
            new Size(788, 36);

        statusLabel.TabIndex = 2;

        statusLabel.Text =
            "Ready";

        statusLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        //
        // HomeView
        //
        AutoScaleDimensions =
            new SizeF(8F, 20F);

        AutoScaleMode =
            AutoScaleMode.Font;

        BackColor =
            Color.FromArgb(245, 247, 250);

        Controls.Add(
            pageLayoutPanel);

        Name =
            "HomeView";

        Size =
            new Size(842, 653);

        pageLayoutPanel.ResumeLayout(false);
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        dashboardTableLayoutPanel.ResumeLayout(false);
        leftColumnTableLayoutPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;

    private Panel headingPanel;
    private Label pageTitleLabel;
    private Label pageDescriptionLabel;
    private Button refreshButton;

    private TableLayoutPanel dashboardTableLayoutPanel;
    private TableLayoutPanel leftColumnTableLayoutPanel;

    private Panel expenseSummaryHostPanel;
    private Panel recentExpensesHostPanel;
    private Panel budgetAccountsHostPanel;

    private Label statusLabel;
}
