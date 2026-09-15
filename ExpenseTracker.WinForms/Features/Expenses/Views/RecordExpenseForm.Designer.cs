namespace ExpenseTracker.WinForms.Features.Expenses.Views;

partial class RecordExpenseForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components =
            new System.ComponentModel.Container();

        pageLayoutPanel = new TableLayoutPanel();
        headingPanel = new Panel();
        descriptionLabel = new Label();
        titleLabel = new Label();
        expenseTitleLabel = new Label();
        titleTextBox = new TextBox();
        notesLabel = new Label();
        notesTextBox = new TextBox();
        selectedItemsLabel = new Label();
        selectedItemsFlowLayoutPanel =
            new FlowLayoutPanel();
        availableItemsLabel = new Label();
        availableItemsFlowLayoutPanel =
            new FlowLayoutPanel();
        footerPanel = new Panel();
        footerButtonsFlowLayoutPanel =
            new FlowLayoutPanel();
        saveButton = new Button();
        cancelButton = new Button();
        estimatedTotalLabel = new Label();
        errorProvider =
            new ErrorProvider(components);

        pageLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        footerPanel.SuspendLayout();
        footerButtonsFlowLayoutPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)
            errorProvider).BeginInit();
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
            expenseTitleLabel,
            0,
            1);
        pageLayoutPanel.Controls.Add(
            titleTextBox,
            0,
            2);
        pageLayoutPanel.Controls.Add(
            notesLabel,
            0,
            3);
        pageLayoutPanel.Controls.Add(
            notesTextBox,
            0,
            4);
        pageLayoutPanel.Controls.Add(
            selectedItemsLabel,
            0,
            5);
        pageLayoutPanel.Controls.Add(
            selectedItemsFlowLayoutPanel,
            0,
            6);
        pageLayoutPanel.Controls.Add(
            availableItemsLabel,
            0,
            7);
        pageLayoutPanel.Controls.Add(
            availableItemsFlowLayoutPanel,
            0,
            8);
        pageLayoutPanel.Controls.Add(
            footerPanel,
            0,
            9);
        pageLayoutPanel.Dock = DockStyle.Fill;
        pageLayoutPanel.Location =
            new Point(0, 0);
        pageLayoutPanel.Name =
            "pageLayoutPanel";
        pageLayoutPanel.Padding =
            new Padding(24);
        pageLayoutPanel.RowCount = 10;
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                76F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                26F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                40F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                26F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                80F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                38F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                45F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                38F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                55F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                72F));
        pageLayoutPanel.Size =
            new Size(900, 760);
        pageLayoutPanel.TabIndex = 0;

        // 
        // headingPanel
        // 
        headingPanel.Controls.Add(
            descriptionLabel);
        headingPanel.Controls.Add(
            titleLabel);
        headingPanel.Dock = DockStyle.Fill;
        headingPanel.Location =
            new Point(27, 27);
        headingPanel.Name =
            "headingPanel";
        headingPanel.Size =
            new Size(846, 70);
        headingPanel.TabIndex = 0;

        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font(
            "Segoe UI",
            18F,
            FontStyle.Bold);
        titleLabel.ForeColor =
            Color.FromArgb(32, 40, 48);
        titleLabel.Location =
            new Point(-3, -5);
        titleLabel.Name = "titleLabel";
        titleLabel.Size =
            new Size(253, 41);
        titleLabel.TabIndex = 0;
        titleLabel.Text =
            "Record Expense";

        // 
        // descriptionLabel
        // 
        descriptionLabel.AutoSize = true;
        descriptionLabel.ForeColor =
            Color.DimGray;
        descriptionLabel.Location =
            new Point(0, 40);
        descriptionLabel.Name =
            "descriptionLabel";
        descriptionLabel.Size =
            new Size(403, 20);
        descriptionLabel.TabIndex = 1;
        descriptionLabel.Text =
            "Choose items below and adjust their quantities directly.";

        // 
        // expenseTitleLabel
        // 
        expenseTitleLabel.Dock =
            DockStyle.Fill;
        expenseTitleLabel.Location =
            new Point(27, 100);
        expenseTitleLabel.Name =
            "expenseTitleLabel";
        expenseTitleLabel.Size =
            new Size(846, 26);
        expenseTitleLabel.TabIndex = 1;
        expenseTitleLabel.Text =
            "Expense Title";
        expenseTitleLabel.TextAlign =
            ContentAlignment.BottomLeft;

        // 
        // titleTextBox
        // 
        titleTextBox.Dock = DockStyle.Top;
        titleTextBox.Location =
            new Point(27, 129);
        titleTextBox.MaxLength = 100;
        titleTextBox.Name =
            "titleTextBox";
        titleTextBox.PlaceholderText =
            "Example: Office Supplies";
        titleTextBox.Size =
            new Size(846, 27);
        titleTextBox.TabIndex = 2;

        // 
        // notesLabel
        // 
        notesLabel.Dock = DockStyle.Fill;
        notesLabel.Location =
            new Point(27, 166);
        notesLabel.Name = "notesLabel";
        notesLabel.Size =
            new Size(846, 26);
        notesLabel.TabIndex = 3;
        notesLabel.Text =
            "Notes (optional)";
        notesLabel.TextAlign =
            ContentAlignment.BottomLeft;

        // 
        // notesTextBox
        // 
        notesTextBox.Dock = DockStyle.Fill;
        notesTextBox.Location =
            new Point(27, 195);
        notesTextBox.MaxLength = 500;
        notesTextBox.Multiline = true;
        notesTextBox.Name =
            "notesTextBox";
        notesTextBox.ScrollBars =
            ScrollBars.Vertical;
        notesTextBox.Size =
            new Size(846, 74);
        notesTextBox.TabIndex = 4;

        // 
        // selectedItemsLabel
        // 
        selectedItemsLabel.Dock =
            DockStyle.Fill;
        selectedItemsLabel.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);
        selectedItemsLabel.Location =
            new Point(27, 272);
        selectedItemsLabel.Name =
            "selectedItemsLabel";
        selectedItemsLabel.Size =
            new Size(846, 38);
        selectedItemsLabel.TabIndex = 5;
        selectedItemsLabel.Text =
            "Selected Expense Items";
        selectedItemsLabel.TextAlign =
            ContentAlignment.BottomLeft;

        // 
        // selectedItemsFlowLayoutPanel
        // 
        selectedItemsFlowLayoutPanel.AutoScroll =
            true;
        selectedItemsFlowLayoutPanel.BackColor =
            Color.FromArgb(245, 247, 250);
        selectedItemsFlowLayoutPanel.BorderStyle =
            BorderStyle.FixedSingle;
        selectedItemsFlowLayoutPanel.Dock =
            DockStyle.Fill;
        selectedItemsFlowLayoutPanel.FlowDirection =
            FlowDirection.TopDown;
        selectedItemsFlowLayoutPanel.Location =
            new Point(27, 313);
        selectedItemsFlowLayoutPanel.Name =
            "selectedItemsFlowLayoutPanel";
        selectedItemsFlowLayoutPanel.Padding =
            new Padding(8);
        selectedItemsFlowLayoutPanel.Size =
            new Size(846, 145);
        selectedItemsFlowLayoutPanel.TabIndex = 6;
        selectedItemsFlowLayoutPanel.WrapContents =
            false;

        // 
        // availableItemsLabel
        // 
        availableItemsLabel.Dock =
            DockStyle.Fill;
        availableItemsLabel.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);
        availableItemsLabel.Location =
            new Point(27, 461);
        availableItemsLabel.Name =
            "availableItemsLabel";
        availableItemsLabel.Size =
            new Size(846, 38);
        availableItemsLabel.TabIndex = 7;
        availableItemsLabel.Text =
            "Available Items";
        availableItemsLabel.TextAlign =
            ContentAlignment.BottomLeft;

        // 
        // availableItemsFlowLayoutPanel
        // 
        availableItemsFlowLayoutPanel.AutoScroll =
            true;
        availableItemsFlowLayoutPanel.BackColor =
            Color.FromArgb(245, 247, 250);
        availableItemsFlowLayoutPanel.BorderStyle =
            BorderStyle.FixedSingle;
        availableItemsFlowLayoutPanel.Dock =
            DockStyle.Fill;
        availableItemsFlowLayoutPanel.FlowDirection =
            FlowDirection.TopDown;
        availableItemsFlowLayoutPanel.Location =
            new Point(27, 502);
        availableItemsFlowLayoutPanel.Name =
            "availableItemsFlowLayoutPanel";
        availableItemsFlowLayoutPanel.Padding =
            new Padding(8);
        availableItemsFlowLayoutPanel.Size =
            new Size(846, 159);
        availableItemsFlowLayoutPanel.TabIndex = 8;
        availableItemsFlowLayoutPanel.WrapContents =
            false;

        // 
        // footerPanel
        // 
        footerPanel.Controls.Add(
            footerButtonsFlowLayoutPanel);
        footerPanel.Controls.Add(
            estimatedTotalLabel);
        footerPanel.Dock = DockStyle.Fill;
        footerPanel.Location =
            new Point(27, 667);
        footerPanel.Name =
            "footerPanel";
        footerPanel.Size =
            new Size(846, 66);
        footerPanel.TabIndex = 9;

        // 
        // estimatedTotalLabel
        // 
        estimatedTotalLabel.Anchor =
            AnchorStyles.Left;
        estimatedTotalLabel.AutoSize =
            true;
        estimatedTotalLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);
        estimatedTotalLabel.ForeColor =
            Color.Green;
        estimatedTotalLabel.Location =
            new Point(0, 18);
        estimatedTotalLabel.Name =
            "estimatedTotalLabel";
        estimatedTotalLabel.Size =
            new Size(221, 28);
        estimatedTotalLabel.TabIndex = 0;
        estimatedTotalLabel.Text =
            "Estimated total: ₱0.00";

        // 
        // footerButtonsFlowLayoutPanel
        // 
        footerButtonsFlowLayoutPanel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;
        footerButtonsFlowLayoutPanel.AutoSize =
            true;
        footerButtonsFlowLayoutPanel.AutoSizeMode =
            AutoSizeMode.GrowAndShrink;
        footerButtonsFlowLayoutPanel.Controls.Add(
            saveButton);
        footerButtonsFlowLayoutPanel.Controls.Add(
            cancelButton);
        footerButtonsFlowLayoutPanel.FlowDirection =
            FlowDirection.RightToLeft;
        footerButtonsFlowLayoutPanel.Location =
            new Point(598, 13);
        footerButtonsFlowLayoutPanel.Name =
            "footerButtonsFlowLayoutPanel";
        footerButtonsFlowLayoutPanel.Size =
            new Size(248, 40);
        footerButtonsFlowLayoutPanel.TabIndex = 1;
        footerButtonsFlowLayoutPanel.WrapContents =
            false;

        // 
        // saveButton
        // 
        saveButton.BackColor =
            Color.Green;
        saveButton.Cursor =
            Cursors.Hand;
        saveButton.Enabled = false;
        saveButton.FlatAppearance.BorderSize =
            0;
        saveButton.FlatStyle =
            FlatStyle.Flat;
        saveButton.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);
        saveButton.ForeColor =
            Color.White;
        saveButton.Margin =
            new Padding(0);
        saveButton.Name =
            "saveButton";
        saveButton.Size =
            new Size(132, 40);
        saveButton.TabIndex = 0;
        saveButton.Text =
            "Save Expense";
        saveButton.UseVisualStyleBackColor =
            false;
        saveButton.Click +=
            saveButton_Click;

        // 
        // cancelButton
        // 
        cancelButton.Cursor =
            Cursors.Hand;
        cancelButton.DialogResult =
            DialogResult.Cancel;
        cancelButton.Margin =
            new Padding(0, 0, 8, 0);
        cancelButton.Name =
            "cancelButton";
        cancelButton.Size =
            new Size(108, 40);
        cancelButton.TabIndex = 1;
        cancelButton.Text =
            "Cancel";
        cancelButton.UseVisualStyleBackColor =
            true;

        // 
        // errorProvider
        // 
        errorProvider.BlinkStyle =
            ErrorBlinkStyle.NeverBlink;
        errorProvider.ContainerControl =
            this;

        // 
        // RecordExpenseForm
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions =
            new SizeF(8F, 20F);
        AutoScaleMode =
            AutoScaleMode.Font;
        BackColor = Color.White;
        CancelButton = cancelButton;
        ClientSize =
            new Size(900, 760);
        Controls.Add(
            pageLayoutPanel);
        FormBorderStyle =
            FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimizeBox = false;
        MinimumSize =
            new Size(820, 680);
        Name =
            "RecordExpenseForm";
        ShowInTaskbar = false;
        StartPosition =
            FormStartPosition.CenterParent;
        Text = "Record Expense";

        pageLayoutPanel.ResumeLayout(false);
        pageLayoutPanel.PerformLayout();
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        footerPanel.ResumeLayout(false);
        footerPanel.PerformLayout();
        footerButtonsFlowLayoutPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)
            errorProvider).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;
    private Panel headingPanel;
    private Label titleLabel;
    private Label descriptionLabel;
    private Label expenseTitleLabel;
    private TextBox titleTextBox;
    private Label notesLabel;
    private TextBox notesTextBox;
    private Label selectedItemsLabel;
    private FlowLayoutPanel selectedItemsFlowLayoutPanel;
    private Label availableItemsLabel;
    private FlowLayoutPanel availableItemsFlowLayoutPanel;
    private Panel footerPanel;
    private Label estimatedTotalLabel;
    private FlowLayoutPanel footerButtonsFlowLayoutPanel;
    private Button saveButton;
    private Button cancelButton;
    private ErrorProvider errorProvider;
}
