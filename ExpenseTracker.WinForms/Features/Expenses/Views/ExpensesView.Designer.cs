namespace ExpenseTracker.WinForms.Features.Expenses.Views;

partial class ExpensesView
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        pageLayoutPanel = new TableLayoutPanel();
        headingPanel = new Panel();
        pageDescriptionLabel = new Label();
        pageTitleLabel = new Label();
        toolbarPanel = new Panel();
        recordExpenseButton = new Button();
        refreshButton = new Button();
        expensesFlowLayoutPanel = new FlowLayoutPanel();
        statusLabel = new Label();
        pageLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        toolbarPanel.SuspendLayout();
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
            toolbarPanel,
            0,
            1);
        pageLayoutPanel.Controls.Add(
            expensesFlowLayoutPanel,
            0,
            2);
        pageLayoutPanel.Controls.Add(
            statusLabel,
            0,
            3);
        pageLayoutPanel.Dock = DockStyle.Fill;
        pageLayoutPanel.Location = new Point(0, 0);
        pageLayoutPanel.Name = "pageLayoutPanel";
        pageLayoutPanel.Padding = new Padding(24);
        pageLayoutPanel.RowCount = 4;
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                82F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                64F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                36F));
        pageLayoutPanel.Size = new Size(842, 653);
        pageLayoutPanel.TabIndex = 0;

        // 
        // headingPanel
        // 
        headingPanel.Controls.Add(
            pageDescriptionLabel);
        headingPanel.Controls.Add(
            pageTitleLabel);
        headingPanel.Dock = DockStyle.Fill;
        headingPanel.Location = new Point(27, 27);
        headingPanel.Name = "headingPanel";
        headingPanel.Size = new Size(788, 76);
        headingPanel.TabIndex = 0;

        // 
        // pageDescriptionLabel
        // 
        pageDescriptionLabel.AutoSize = true;
        pageDescriptionLabel.ForeColor =
            Color.DimGray;
        pageDescriptionLabel.Location =
            new Point(0, 43);
        pageDescriptionLabel.Name =
            "pageDescriptionLabel";
        pageDescriptionLabel.Size =
            new Size(360, 20);
        pageDescriptionLabel.TabIndex = 1;
        pageDescriptionLabel.Text =
            "Review recorded expenses and their historical item details.";

        // 
        // pageTitleLabel
        // 
        pageTitleLabel.AutoSize = true;
        pageTitleLabel.Font = new Font(
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
            new Size(173, 46);
        pageTitleLabel.TabIndex = 0;
        pageTitleLabel.Text = "Expenses";

        // 
        // toolbarPanel
        // 
        toolbarPanel.Controls.Add(
            recordExpenseButton);
        toolbarPanel.Controls.Add(
            refreshButton);
        toolbarPanel.Dock = DockStyle.Fill;
        toolbarPanel.Location =
            new Point(27, 109);
        toolbarPanel.Name = "toolbarPanel";
        toolbarPanel.Size =
            new Size(788, 58);
        toolbarPanel.TabIndex = 1;

        // 
        // recordExpenseButton
        // 
        recordExpenseButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;
        recordExpenseButton.BackColor =
            Color.Green;
        recordExpenseButton.Cursor =
            Cursors.Hand;
        recordExpenseButton.FlatAppearance
            .BorderSize = 0;
        recordExpenseButton.FlatStyle =
            FlatStyle.Flat;
        recordExpenseButton.Font = new Font(
            "Segoe UI",
            9F,
            FontStyle.Bold);
        recordExpenseButton.ForeColor =
            Color.White;
        recordExpenseButton.Location =
            new Point(632, 10);
        recordExpenseButton.Name =
            "recordExpenseButton";
        recordExpenseButton.Size =
            new Size(156, 40);
        recordExpenseButton.TabIndex = 1;
        recordExpenseButton.Text =
            "Record Expense";
        recordExpenseButton
            .UseVisualStyleBackColor = false;

        // 
        // refreshButton
        // 
        refreshButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;
        refreshButton.Cursor =
            Cursors.Hand;
        refreshButton.Location =
            new Point(528, 10);
        refreshButton.Name =
            "refreshButton";
        refreshButton.Size =
            new Size(94, 40);
        refreshButton.TabIndex = 0;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor =
            true;

        // 
        // expensesFlowLayoutPanel
        // 
        expensesFlowLayoutPanel.AutoScroll =
            true;
        expensesFlowLayoutPanel.BackColor =
            Color.FromArgb(245, 247, 250);
        expensesFlowLayoutPanel.Dock =
            DockStyle.Fill;
        expensesFlowLayoutPanel.FlowDirection =
            FlowDirection.TopDown;
        expensesFlowLayoutPanel.Location =
            new Point(27, 173);
        expensesFlowLayoutPanel.Name =
            "expensesFlowLayoutPanel";
        expensesFlowLayoutPanel.Padding =
            new Padding(0, 0, 8, 8);
        expensesFlowLayoutPanel.Size =
            new Size(788, 417);
        expensesFlowLayoutPanel.TabIndex = 2;
        expensesFlowLayoutPanel.WrapContents =
            false;

        // 
        // statusLabel
        // 
        statusLabel.Dock = DockStyle.Fill;
        statusLabel.ForeColor =
            Color.DimGray;
        statusLabel.Location =
            new Point(27, 593);
        statusLabel.Name = "statusLabel";
        statusLabel.Size =
            new Size(788, 36);
        statusLabel.TabIndex = 3;
        statusLabel.Text = "Ready";
        statusLabel.TextAlign =
            ContentAlignment.MiddleLeft;

        // 
        // ExpensesView
        // 
        AutoScaleDimensions =
            new SizeF(8F, 20F);
        AutoScaleMode =
            AutoScaleMode.Font;
        BackColor =
            Color.FromArgb(245, 247, 250);
        Controls.Add(pageLayoutPanel);
        Name = "ExpensesView";
        Size = new Size(842, 653);

        pageLayoutPanel.ResumeLayout(false);
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        toolbarPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;
    private Panel headingPanel;
    private Label pageTitleLabel;
    private Label pageDescriptionLabel;
    private Panel toolbarPanel;
    private Button refreshButton;
    private Button recordExpenseButton;
    private FlowLayoutPanel expensesFlowLayoutPanel;
    private Label statusLabel;
}
