namespace ExpenseTracker.WinForms.Features.Home.RecentExpenses;

partial class RecentExpensesPanel
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

        headingPanel =
            new Panel();

        titleLabel =
            new Label();

        descriptionLabel =
            new Label();

        recentExpensesFlowLayoutPanel =
            new FlowLayoutPanel();

        outerPanel.SuspendLayout();
        contentLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
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
            new Size(480, 257);

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
            headingPanel,
            0,
            0);

        contentLayoutPanel.Controls.Add(
            recentExpensesFlowLayoutPanel,
            0,
            1);

        contentLayoutPanel.Dock =
            DockStyle.Fill;

        contentLayoutPanel.Location =
            new Point(18, 18);

        contentLayoutPanel.Margin =
            new Padding(0);

        contentLayoutPanel.Name =
            "contentLayoutPanel";

        contentLayoutPanel.RowCount = 2;

        contentLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                64F));

        contentLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        contentLayoutPanel.Size =
            new Size(442, 219);

        contentLayoutPanel.TabIndex = 0;

        //
        // headingPanel
        //
        headingPanel.Controls.Add(
            descriptionLabel);

        headingPanel.Controls.Add(
            titleLabel);

        headingPanel.Dock =
            DockStyle.Fill;

        headingPanel.Location =
            new Point(0, 0);

        headingPanel.Margin =
            new Padding(0);

        headingPanel.Name =
            "headingPanel";

        headingPanel.Size =
            new Size(442, 64);

        headingPanel.TabIndex = 0;

        //
        // titleLabel
        //
        titleLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        titleLabel.AutoEllipsis =
            true;

        titleLabel.Font =
            new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

        titleLabel.Location =
            new Point(0, 0);

        titleLabel.Name =
            "titleLabel";

        titleLabel.Size =
            new Size(442, 29);

        titleLabel.TabIndex = 0;

        titleLabel.Text =
            "Recent Expense Activity";

        //
        // descriptionLabel
        //
        descriptionLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        descriptionLabel.AutoEllipsis =
            true;

        descriptionLabel.ForeColor =
            Color.DimGray;

        descriptionLabel.Location =
            new Point(1, 33);

        descriptionLabel.Name =
            "descriptionLabel";

        descriptionLabel.Size =
            new Size(441, 24);

        descriptionLabel.TabIndex = 1;

        descriptionLabel.Text =
            "The five most recently recorded expenses.";

        //
        // recentExpensesFlowLayoutPanel
        //
        recentExpensesFlowLayoutPanel.AutoScroll =
            true;

        recentExpensesFlowLayoutPanel.BackColor =
            Color.FromArgb(245, 247, 250);

        recentExpensesFlowLayoutPanel.Dock =
            DockStyle.Fill;

        recentExpensesFlowLayoutPanel.FlowDirection =
            FlowDirection.TopDown;

        recentExpensesFlowLayoutPanel.Location =
            new Point(0, 64);

        recentExpensesFlowLayoutPanel.Margin =
            new Padding(0);

        recentExpensesFlowLayoutPanel.Name =
            "recentExpensesFlowLayoutPanel";

        recentExpensesFlowLayoutPanel.Padding =
            new Padding(8);

        recentExpensesFlowLayoutPanel.Size =
            new Size(442, 155);

        recentExpensesFlowLayoutPanel.TabIndex = 1;

        recentExpensesFlowLayoutPanel.WrapContents =
            false;

        //
        // RecentExpensesPanel
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
            "RecentExpensesPanel";

        Size =
            new Size(480, 257);

        outerPanel.ResumeLayout(false);
        contentLayoutPanel.ResumeLayout(false);
        headingPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel outerPanel;
    private TableLayoutPanel contentLayoutPanel;

    private Panel headingPanel;
    private Label titleLabel;
    private Label descriptionLabel;

    private FlowLayoutPanel recentExpensesFlowLayoutPanel;
}
