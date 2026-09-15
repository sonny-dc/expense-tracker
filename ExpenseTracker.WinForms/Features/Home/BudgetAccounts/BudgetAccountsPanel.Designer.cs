namespace ExpenseTracker.WinForms.Features.Home.BudgetAccounts;

partial class BudgetAccountsPanel
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
        outerPanel =
            new Panel();

        contentLayoutPanel =
            new TableLayoutPanel();

        titleLabel =
            new Label();

        descriptionLabel =
            new Label();

        budgetAccountsFlowLayoutPanel =
            new FlowLayoutPanel();

        emptyBudgetPanel =
            new Panel();

        emptyBudgetTitleLabel =
            new Label();

        emptyBudgetDescriptionLabel =
            new Label();

        outerPanel.SuspendLayout();
        contentLayoutPanel.SuspendLayout();
        budgetAccountsFlowLayoutPanel.SuspendLayout();
        emptyBudgetPanel.SuspendLayout();
        SuspendLayout();

        //
        // outerPanel
        //
        outerPanel.BackColor =
            Color.White;

        outerPanel.BorderStyle =
            BorderStyle.FixedSingle;

        outerPanel.Controls.Add(
            contentLayoutPanel);

        outerPanel.Dock =
            DockStyle.Fill;

        outerPanel.Location =
            new Point(0, 0);

        outerPanel.Name =
            "outerPanel";

        outerPanel.Padding =
            new Padding(18);

        outerPanel.Size =
            new Size(292, 481);

        outerPanel.TabIndex = 0;

        //
        // contentLayoutPanel
        //
        contentLayoutPanel.ColumnCount = 1;

        contentLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100F));

        contentLayoutPanel.Controls.Add(
            titleLabel,
            0,
            0);

        contentLayoutPanel.Controls.Add(
            descriptionLabel,
            0,
            1);

        contentLayoutPanel.Controls.Add(
            budgetAccountsFlowLayoutPanel,
            0,
            2);

        contentLayoutPanel.Dock =
            DockStyle.Fill;

        contentLayoutPanel.Location =
            new Point(18, 18);

        contentLayoutPanel.Margin =
            new Padding(0);

        contentLayoutPanel.Name =
            "contentLayoutPanel";

        contentLayoutPanel.RowCount = 3;

        contentLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                36F));

        contentLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                58F));

        contentLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        contentLayoutPanel.Size =
            new Size(254, 443);

        contentLayoutPanel.TabIndex = 0;

        //
        // titleLabel
        //
        titleLabel.AutoSize =
            true;

        titleLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);

        titleLabel.Location =
            new Point(0, 0);

        titleLabel.Margin =
            new Padding(0);

        titleLabel.Name =
            "titleLabel";

        titleLabel.Size =
            new Size(175, 28);

        titleLabel.TabIndex = 0;

        titleLabel.Text =
            "Budget Accounts";

        //
        // descriptionLabel
        //
        descriptionLabel.Dock =
            DockStyle.Fill;

        descriptionLabel.ForeColor =
            Color.DimGray;

        descriptionLabel.Location =
            new Point(0, 36);

        descriptionLabel.Margin =
            new Padding(0);

        descriptionLabel.Name =
            "descriptionLabel";

        descriptionLabel.Size =
            new Size(254, 58);

        descriptionLabel.TabIndex = 1;

        descriptionLabel.Text =
            "Available budgets will be displayed in this area.";

        //
        // budgetAccountsFlowLayoutPanel
        //
        budgetAccountsFlowLayoutPanel.AutoScroll =
            true;

        budgetAccountsFlowLayoutPanel.BackColor =
            Color.FromArgb(245, 247, 250);

        budgetAccountsFlowLayoutPanel.Controls.Add(
            emptyBudgetPanel);

        budgetAccountsFlowLayoutPanel.Dock =
            DockStyle.Fill;

        budgetAccountsFlowLayoutPanel.FlowDirection =
            FlowDirection.TopDown;

        budgetAccountsFlowLayoutPanel.Location =
            new Point(0, 94);

        budgetAccountsFlowLayoutPanel.Margin =
            new Padding(0);

        budgetAccountsFlowLayoutPanel.Name =
            "budgetAccountsFlowLayoutPanel";

        budgetAccountsFlowLayoutPanel.Padding =
            new Padding(8);

        budgetAccountsFlowLayoutPanel.Size =
            new Size(254, 349);

        budgetAccountsFlowLayoutPanel.TabIndex = 2;

        budgetAccountsFlowLayoutPanel.WrapContents =
            false;

        //
        // emptyBudgetPanel
        //
        emptyBudgetPanel.BackColor =
            Color.White;

        emptyBudgetPanel.BorderStyle =
            BorderStyle.FixedSingle;

        emptyBudgetPanel.Controls.Add(
            emptyBudgetDescriptionLabel);

        emptyBudgetPanel.Controls.Add(
            emptyBudgetTitleLabel);

        emptyBudgetPanel.Margin =
            new Padding(0, 0, 0, 10);

        emptyBudgetPanel.Name =
            "emptyBudgetPanel";

        emptyBudgetPanel.Size =
            new Size(218, 132);

        emptyBudgetPanel.TabIndex = 0;

        //
        // emptyBudgetTitleLabel
        //
        emptyBudgetTitleLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        emptyBudgetTitleLabel.AutoEllipsis =
            true;

        emptyBudgetTitleLabel.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

        emptyBudgetTitleLabel.Location =
            new Point(14, 14);

        emptyBudgetTitleLabel.Name =
            "emptyBudgetTitleLabel";

        emptyBudgetTitleLabel.Size =
            new Size(187, 26);

        emptyBudgetTitleLabel.TabIndex = 0;

        emptyBudgetTitleLabel.Text =
            "No budgets yet";

        //
        // emptyBudgetDescriptionLabel
        //
        emptyBudgetDescriptionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        emptyBudgetDescriptionLabel.AutoEllipsis =
            true;

        emptyBudgetDescriptionLabel.ForeColor =
            Color.DimGray;

        emptyBudgetDescriptionLabel.Location =
            new Point(14, 48);

        emptyBudgetDescriptionLabel.Name =
            "emptyBudgetDescriptionLabel";

        emptyBudgetDescriptionLabel.Size =
            new Size(187, 67);

        emptyBudgetDescriptionLabel.TabIndex = 1;

        emptyBudgetDescriptionLabel.Text =
            "Budget accounts will appear here after budget tracking is added.";

        //
        // BudgetAccountsPanel
        //
        AutoScaleDimensions =
            new SizeF(8F, 20F);

        AutoScaleMode =
            AutoScaleMode.Font;

        BackColor =
            Color.FromArgb(245, 247, 250);

        Controls.Add(
            outerPanel);

        Name =
            "BudgetAccountsPanel";

        Size =
            new Size(292, 481);

        outerPanel.ResumeLayout(false);
        contentLayoutPanel.ResumeLayout(false);
        contentLayoutPanel.PerformLayout();
        budgetAccountsFlowLayoutPanel.ResumeLayout(false);
        emptyBudgetPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel outerPanel;
    private TableLayoutPanel contentLayoutPanel;

    private Label titleLabel;
    private Label descriptionLabel;

    private FlowLayoutPanel budgetAccountsFlowLayoutPanel;

    private Panel emptyBudgetPanel;
    private Label emptyBudgetTitleLabel;
    private Label emptyBudgetDescriptionLabel;
}
