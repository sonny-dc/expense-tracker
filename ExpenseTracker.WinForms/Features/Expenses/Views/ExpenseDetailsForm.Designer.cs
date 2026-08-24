namespace ExpenseTracker.WinForms.Features.Expenses.Views;

partial class ExpenseDetailsForm
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
        pageLayoutPanel = new TableLayoutPanel();
        headerPanel = new Panel();
        dateLabel = new Label();
        expenseNumberLabel = new Label();
        titleLabel = new Label();

        summaryTableLayoutPanel = new TableLayoutPanel();

        totalPanel = new Panel();
        totalCaptionLabel = new Label();
        totalValueLabel = new Label();

        itemCountPanel = new Panel();
        itemCountCaptionLabel = new Label();
        itemCountValueLabel = new Label();

        sourceStatusPanel = new Panel();
        sourceStatusCaptionLabel = new Label();
        sourceStatusValueLabel = new Label();

        notesSectionPanel = new Panel();
        notesLabel = new Label();
        notesTextBox = new TextBox();

        itemsSectionPanel = new Panel();
        historicalNoticeLabel = new Label();
        historicalItemsLabel = new Label();

        historicalItemsDataGridView =
            new DataGridView();

        itemNameColumn =
            new DataGridViewTextBoxColumn();

        itemCodeColumn =
            new DataGridViewTextBoxColumn();

        brandColumn =
            new DataGridViewTextBoxColumn();

        quantityColumn =
            new DataGridViewTextBoxColumn();

        unitPriceColumn =
            new DataGridViewTextBoxColumn();

        lineTotalColumn =
            new DataGridViewTextBoxColumn();

        sourceStatusColumn =
            new DataGridViewTextBoxColumn();

        footerPanel = new Panel();
        snapshotNoticeLabel = new Label();
        closeButton = new Button();

        pageLayoutPanel.SuspendLayout();
        headerPanel.SuspendLayout();
        summaryTableLayoutPanel.SuspendLayout();
        totalPanel.SuspendLayout();
        itemCountPanel.SuspendLayout();
        sourceStatusPanel.SuspendLayout();
        notesSectionPanel.SuspendLayout();
        itemsSectionPanel.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)
            historicalItemsDataGridView).BeginInit();

        footerPanel.SuspendLayout();
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
            headerPanel,
            0,
            0);

        pageLayoutPanel.Controls.Add(
            summaryTableLayoutPanel,
            0,
            1);

        pageLayoutPanel.Controls.Add(
            notesSectionPanel,
            0,
            2);

        pageLayoutPanel.Controls.Add(
            itemsSectionPanel,
            0,
            3);

        pageLayoutPanel.Controls.Add(
            historicalItemsDataGridView,
            0,
            4);

        pageLayoutPanel.Controls.Add(
            footerPanel,
            0,
            5);

        pageLayoutPanel.Dock =
            DockStyle.Fill;

        pageLayoutPanel.Location =
            new Point(0, 0);

        pageLayoutPanel.Name =
            "pageLayoutPanel";

        pageLayoutPanel.Padding =
            new Padding(24);

        pageLayoutPanel.RowCount = 6;

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                105F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                112F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                125F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                66F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                70F));

        pageLayoutPanel.Size =
            new Size(1050, 720);

        pageLayoutPanel.TabIndex = 0;

        //
        // headerPanel
        //
        headerPanel.BackColor =
            Color.FromArgb(247, 250, 248);

        headerPanel.Controls.Add(
            dateLabel);

        headerPanel.Controls.Add(
            expenseNumberLabel);

        headerPanel.Controls.Add(
            titleLabel);

        headerPanel.Dock =
            DockStyle.Fill;

        headerPanel.Location =
            new Point(27, 27);

        headerPanel.Name =
            "headerPanel";

        headerPanel.Padding =
            new Padding(20);

        headerPanel.Size =
            new Size(996, 99);

        headerPanel.TabIndex = 0;

        //
        // titleLabel
        //
        titleLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;

        titleLabel.AutoEllipsis = true;
        titleLabel.Font = new Font(
            "Segoe UI",
            20F,
            FontStyle.Bold);

        titleLabel.ForeColor =
            Color.FromArgb(32, 40, 48);

        titleLabel.Location =
            new Point(20, 13);

        titleLabel.Name =
            "titleLabel";

        titleLabel.Size =
            new Size(690, 46);

        titleLabel.TabIndex = 0;

        titleLabel.Text =
            "Expense Title";

        //
        // expenseNumberLabel
        //
        expenseNumberLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        expenseNumberLabel.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

        expenseNumberLabel.ForeColor =
            Color.Green;

        expenseNumberLabel.Location =
            new Point(754, 17);

        expenseNumberLabel.Name =
            "expenseNumberLabel";

        expenseNumberLabel.Size =
            new Size(222, 27);

        expenseNumberLabel.TabIndex = 1;

        expenseNumberLabel.Text =
            "Expense #0";

        expenseNumberLabel.TextAlign =
            ContentAlignment.MiddleRight;

        //
        // dateLabel
        //
        dateLabel.AutoSize = true;
        dateLabel.ForeColor =
            Color.DimGray;

        dateLabel.Location =
            new Point(22, 66);

        dateLabel.Name =
            "dateLabel";

        dateLabel.Size =
            new Size(142, 20);

        dateLabel.TabIndex = 2;

        dateLabel.Text =
            "January 1, 2026";

        //
        // summaryTableLayoutPanel
        //
        summaryTableLayoutPanel.ColumnCount = 3;

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.33333F));

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.33333F));

        summaryTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                33.33333F));

        summaryTableLayoutPanel.Controls.Add(
            totalPanel,
            0,
            0);

        summaryTableLayoutPanel.Controls.Add(
            itemCountPanel,
            1,
            0);

        summaryTableLayoutPanel.Controls.Add(
            sourceStatusPanel,
            2,
            0);

        summaryTableLayoutPanel.Dock =
            DockStyle.Fill;

        summaryTableLayoutPanel.Location =
            new Point(27, 132);

        summaryTableLayoutPanel.Name =
            "summaryTableLayoutPanel";

        summaryTableLayoutPanel.RowCount = 1;

        summaryTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));

        summaryTableLayoutPanel.Size =
            new Size(996, 106);

        summaryTableLayoutPanel.TabIndex = 1;

        //
        // totalPanel
        //
        totalPanel.BackColor = Color.White;
        totalPanel.BorderStyle =
            BorderStyle.FixedSingle;

        totalPanel.Controls.Add(
            totalValueLabel);

        totalPanel.Controls.Add(
            totalCaptionLabel);

        totalPanel.Dock =
            DockStyle.Fill;

        totalPanel.Margin =
            new Padding(0, 6, 8, 6);

        totalPanel.Name =
            "totalPanel";

        totalPanel.TabIndex = 0;

        //
        // totalCaptionLabel
        //
        totalCaptionLabel.AutoSize = true;
        totalCaptionLabel.Font =
            new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);

        totalCaptionLabel.ForeColor =
            Color.DimGray;

        totalCaptionLabel.Location =
            new Point(18, 15);

        totalCaptionLabel.Name =
            "totalCaptionLabel";

        totalCaptionLabel.Size =
            new Size(53, 20);

        totalCaptionLabel.TabIndex = 0;

        totalCaptionLabel.Text =
            "TOTAL";

        //
        // totalValueLabel
        //
        totalValueLabel.AutoEllipsis = true;
        totalValueLabel.Font =
            new Font(
                "Segoe UI",
                17F,
                FontStyle.Bold);

        totalValueLabel.ForeColor =
            Color.Green;

        totalValueLabel.Location =
            new Point(16, 43);

        totalValueLabel.Name =
            "totalValueLabel";

        totalValueLabel.Size =
            new Size(280, 42);

        totalValueLabel.TabIndex = 1;

        totalValueLabel.Text =
            "₱0.00";

        //
        // itemCountPanel
        //
        itemCountPanel.BackColor =
            Color.White;

        itemCountPanel.BorderStyle =
            BorderStyle.FixedSingle;

        itemCountPanel.Controls.Add(
            itemCountValueLabel);

        itemCountPanel.Controls.Add(
            itemCountCaptionLabel);

        itemCountPanel.Dock =
            DockStyle.Fill;

        itemCountPanel.Margin =
            new Padding(4, 6, 4, 6);

        itemCountPanel.Name =
            "itemCountPanel";

        itemCountPanel.TabIndex = 1;

        //
        // itemCountCaptionLabel
        //
        itemCountCaptionLabel.AutoSize =
            true;

        itemCountCaptionLabel.Font =
            new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);

        itemCountCaptionLabel.ForeColor =
            Color.DimGray;

        itemCountCaptionLabel.Location =
            new Point(18, 15);

        itemCountCaptionLabel.Name =
            "itemCountCaptionLabel";

        itemCountCaptionLabel.Size =
            new Size(85, 20);

        itemCountCaptionLabel.TabIndex = 0;

        itemCountCaptionLabel.Text =
            "ITEM LINES";

        //
        // itemCountValueLabel
        //
        itemCountValueLabel.Font =
            new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold);

        itemCountValueLabel.ForeColor =
            Color.FromArgb(32, 40, 48);

        itemCountValueLabel.Location =
            new Point(16, 45);

        itemCountValueLabel.Name =
            "itemCountValueLabel";

        itemCountValueLabel.Size =
            new Size(280, 38);

        itemCountValueLabel.TabIndex = 1;

        itemCountValueLabel.Text =
            "0 items";

        //
        // sourceStatusPanel
        //
        sourceStatusPanel.BackColor =
            Color.White;

        sourceStatusPanel.BorderStyle =
            BorderStyle.FixedSingle;

        sourceStatusPanel.Controls.Add(
            sourceStatusValueLabel);

        sourceStatusPanel.Controls.Add(
            sourceStatusCaptionLabel);

        sourceStatusPanel.Dock =
            DockStyle.Fill;

        sourceStatusPanel.Margin =
            new Padding(8, 6, 0, 6);

        sourceStatusPanel.Name =
            "sourceStatusPanel";

        sourceStatusPanel.TabIndex = 2;

        //
        // sourceStatusCaptionLabel
        //
        sourceStatusCaptionLabel.AutoSize =
            true;

        sourceStatusCaptionLabel.Font =
            new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);

        sourceStatusCaptionLabel.ForeColor =
            Color.DimGray;

        sourceStatusCaptionLabel.Location =
            new Point(18, 15);

        sourceStatusCaptionLabel.Name =
            "sourceStatusCaptionLabel";

        sourceStatusCaptionLabel.Size =
            new Size(119, 20);

        sourceStatusCaptionLabel.TabIndex = 0;

        sourceStatusCaptionLabel.Text =
            "SOURCE STATUS";

        //
        // sourceStatusValueLabel
        //
        sourceStatusValueLabel.AutoEllipsis =
            true;

        sourceStatusValueLabel.Font =
            new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold);

        sourceStatusValueLabel.ForeColor =
            Color.Green;

        sourceStatusValueLabel.Location =
            new Point(16, 47);

        sourceStatusValueLabel.Name =
            "sourceStatusValueLabel";

        sourceStatusValueLabel.Size =
            new Size(280, 34);

        sourceStatusValueLabel.TabIndex = 1;

        sourceStatusValueLabel.Text =
            "All available";

        //
        // notesSectionPanel
        //
        notesSectionPanel.BackColor =
            Color.White;

        notesSectionPanel.BorderStyle =
            BorderStyle.FixedSingle;

        notesSectionPanel.Controls.Add(
            notesTextBox);

        notesSectionPanel.Controls.Add(
            notesLabel);

        notesSectionPanel.Dock =
            DockStyle.Fill;

        notesSectionPanel.Location =
            new Point(27, 244);

        notesSectionPanel.Name =
            "notesSectionPanel";

        notesSectionPanel.Padding =
            new Padding(16);

        notesSectionPanel.Size =
            new Size(996, 119);

        notesSectionPanel.TabIndex = 2;

        //
        // notesLabel
        //
        notesLabel.AutoSize = true;
        notesLabel.Font =
            new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

        notesLabel.Location =
            new Point(16, 12);

        notesLabel.Name =
            "notesLabel";

        notesLabel.Size =
            new Size(55, 23);

        notesLabel.TabIndex = 0;

        notesLabel.Text =
            "Notes";

        //
        // notesTextBox
        //
        notesTextBox.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Bottom |
            AnchorStyles.Left |
            AnchorStyles.Right;

        notesTextBox.BackColor =
            Color.White;

        notesTextBox.BorderStyle =
            BorderStyle.None;

        notesTextBox.Location =
            new Point(18, 44);

        notesTextBox.Multiline = true;
        notesTextBox.Name =
            "notesTextBox";

        notesTextBox.ReadOnly = true;

        notesTextBox.TabStop = false;

        notesTextBox.ScrollBars =
            ScrollBars.Vertical;

        notesTextBox.Size =
            new Size(958, 59);

        notesTextBox.TabIndex = 1;

        //
        // itemsSectionPanel
        //
        itemsSectionPanel.Controls.Add(
            historicalNoticeLabel);

        itemsSectionPanel.Controls.Add(
            historicalItemsLabel);

        itemsSectionPanel.Dock =
            DockStyle.Fill;

        itemsSectionPanel.Location =
            new Point(27, 369);

        itemsSectionPanel.Name =
            "itemsSectionPanel";

        itemsSectionPanel.Size =
            new Size(996, 60);

        itemsSectionPanel.TabIndex = 3;

        //
        // historicalItemsLabel
        //
        historicalItemsLabel.AutoSize =
            true;

        historicalItemsLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);

        historicalItemsLabel.Location =
            new Point(0, 8);

        historicalItemsLabel.Name =
            "historicalItemsLabel";

        historicalItemsLabel.Size =
            new Size(210, 28);

        historicalItemsLabel.TabIndex = 0;

        historicalItemsLabel.Text =
            "Historical Item Details";

        //
        // historicalNoticeLabel
        //
        historicalNoticeLabel.AutoSize =
            true;

        historicalNoticeLabel.ForeColor =
            Color.DimGray;

        historicalNoticeLabel.Location =
            new Point(1, 39);

        historicalNoticeLabel.Name =
            "historicalNoticeLabel";

        historicalNoticeLabel.Size =
            new Size(473, 20);

        historicalNoticeLabel.TabIndex = 1;

        historicalNoticeLabel.Text =
            "Values below reflect the item information when the expense was recorded.";

        //
        // historicalItemsDataGridView
        //
        historicalItemsDataGridView.AllowUserToAddRows =
            false;

        historicalItemsDataGridView.AllowUserToDeleteRows =
            false;

        historicalItemsDataGridView.AllowUserToResizeRows =
            false;

        historicalItemsDataGridView.AutoGenerateColumns =
            false;

        historicalItemsDataGridView.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        historicalItemsDataGridView.BackgroundColor =
            Color.White;

        historicalItemsDataGridView.BorderStyle =
            BorderStyle.FixedSingle;

        historicalItemsDataGridView.ColumnHeadersHeight =
            42;

        historicalItemsDataGridView.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

        historicalItemsDataGridView.Columns.AddRange(
            itemNameColumn,
            itemCodeColumn,
            brandColumn,
            quantityColumn,
            unitPriceColumn,
            lineTotalColumn,
            sourceStatusColumn);

        historicalItemsDataGridView.Dock =
            DockStyle.Fill;

        historicalItemsDataGridView.Location =
            new Point(27, 435);

        historicalItemsDataGridView.MultiSelect =
            false;

        historicalItemsDataGridView.Name =
            "historicalItemsDataGridView";

        historicalItemsDataGridView.ReadOnly =
            true;

        historicalItemsDataGridView.RowHeadersVisible =
            false;

        historicalItemsDataGridView.TabStop =
            false;

        historicalItemsDataGridView.RowTemplate.Height =
            38;

        historicalItemsDataGridView.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        historicalItemsDataGridView.Size =
            new Size(996, 188);

        historicalItemsDataGridView.TabIndex = 4;

        historicalItemsDataGridView.CellFormatting +=
            historicalItemsDataGridView_CellFormatting;

        //
        // itemNameColumn
        //
        itemNameColumn.DataPropertyName =
            "ItemNameSnapshot";

        itemNameColumn.FillWeight = 120F;
        itemNameColumn.HeaderText = "Item";
        itemNameColumn.Name =
            "itemNameColumn";

        itemNameColumn.ReadOnly = true;

        //
        // itemCodeColumn
        //
        itemCodeColumn.DataPropertyName =
            "ItemCodeSnapshot";

        itemCodeColumn.HeaderText = "Code";
        itemCodeColumn.Name =
            "itemCodeColumn";

        itemCodeColumn.ReadOnly = true;

        //
        // brandColumn
        //
        brandColumn.DataPropertyName =
            "BrandSnapshot";

        brandColumn.HeaderText = "Brand";
        brandColumn.Name =
            "brandColumn";

        brandColumn.ReadOnly = true;

        //
        // quantityColumn
        //
        quantityColumn.DataPropertyName =
            "Quantity";

        quantityColumn.FillWeight = 75F;
        quantityColumn.HeaderText = "Quantity";
        quantityColumn.Name =
            "quantityColumn";

        quantityColumn.ReadOnly = true;

        quantityColumn.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Alignment =
                    DataGridViewContentAlignment.MiddleRight
            };

        //
        // unitPriceColumn
        //
        unitPriceColumn.DataPropertyName =
            "UnitPriceSnapshot";

        unitPriceColumn.FillWeight = 90F;
        unitPriceColumn.HeaderText =
            "Unit Price";

        unitPriceColumn.Name =
            "unitPriceColumn";

        unitPriceColumn.ReadOnly = true;

        unitPriceColumn.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Alignment =
                    DataGridViewContentAlignment.MiddleRight
            };

        //
        // lineTotalColumn
        //
        lineTotalColumn.DataPropertyName =
            "LineTotal";

        lineTotalColumn.FillWeight = 90F;
        lineTotalColumn.HeaderText =
            "Line Total";

        lineTotalColumn.Name =
            "lineTotalColumn";

        lineTotalColumn.ReadOnly = true;

        lineTotalColumn.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Alignment =
                    DataGridViewContentAlignment.MiddleRight
            };

        //
        // sourceStatusColumn
        //
        sourceStatusColumn.FillWeight = 80F;
        sourceStatusColumn.HeaderText =
            "Source";

        sourceStatusColumn.Name =
            "sourceStatusColumn";

        sourceStatusColumn.ReadOnly = true;

        sourceStatusColumn.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Alignment =
                    DataGridViewContentAlignment.MiddleCenter
            };

        //
        // footerPanel
        //
        footerPanel.Controls.Add(
            snapshotNoticeLabel);

        footerPanel.Controls.Add(
            closeButton);

        footerPanel.Dock =
            DockStyle.Fill;

        footerPanel.Location =
            new Point(27, 629);

        footerPanel.Name =
            "footerPanel";

        footerPanel.Size =
            new Size(996, 64);

        footerPanel.TabIndex = 5;

        //
        // snapshotNoticeLabel
        //
        snapshotNoticeLabel.Anchor =
            AnchorStyles.Left;

        snapshotNoticeLabel.AutoSize =
            true;

        snapshotNoticeLabel.ForeColor =
            Color.DimGray;

        snapshotNoticeLabel.Location =
            new Point(0, 22);

        snapshotNoticeLabel.Name =
            "snapshotNoticeLabel";

        snapshotNoticeLabel.Size =
            new Size(493, 20);

        snapshotNoticeLabel.TabIndex = 0;

        snapshotNoticeLabel.Text =
            "Historical snapshots are preserved even when a source item is deleted.";

        //
        // closeButton
        //
        closeButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;

        closeButton.Cursor =
            Cursors.Hand;

        closeButton.DialogResult =
            DialogResult.OK;

        closeButton.Location =
            new Point(878, 12);

        closeButton.Name =
            "closeButton";

        closeButton.Size =
            new Size(118, 40);

        closeButton.TabIndex = 1;
        closeButton.Text = "Close";

        closeButton.UseVisualStyleBackColor =
            true;

        //
        // ExpenseDetailsForm
        //
        AcceptButton = closeButton;
        AutoScaleDimensions =
            new SizeF(8F, 20F);

        AutoScaleMode =
            AutoScaleMode.Font;

        BackColor =
            Color.FromArgb(245, 247, 250);

        CancelButton = closeButton;

        ClientSize =
            new Size(1050, 720);

        Controls.Add(
            pageLayoutPanel);

        FormBorderStyle =
            FormBorderStyle.Sizable;

        MaximizeBox = true;
        MinimizeBox = false;

        MinimumSize =
            new Size(900, 650);

        Name =
            "ExpenseDetailsForm";

        ShowInTaskbar = false;

        StartPosition =
            FormStartPosition.CenterParent;

        Text =
            "Expense Details";

        pageLayoutPanel.ResumeLayout(false);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        summaryTableLayoutPanel.ResumeLayout(false);
        totalPanel.ResumeLayout(false);
        totalPanel.PerformLayout();
        itemCountPanel.ResumeLayout(false);
        itemCountPanel.PerformLayout();
        sourceStatusPanel.ResumeLayout(false);
        sourceStatusPanel.PerformLayout();
        notesSectionPanel.ResumeLayout(false);
        notesSectionPanel.PerformLayout();
        itemsSectionPanel.ResumeLayout(false);
        itemsSectionPanel.PerformLayout();

        ((System.ComponentModel.ISupportInitialize)
            historicalItemsDataGridView).EndInit();

        footerPanel.ResumeLayout(false);
        footerPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;
    private Panel headerPanel;
    private Label titleLabel;
    private Label expenseNumberLabel;
    private Label dateLabel;

    private TableLayoutPanel summaryTableLayoutPanel;

    private Panel totalPanel;
    private Label totalCaptionLabel;
    private Label totalValueLabel;

    private Panel itemCountPanel;
    private Label itemCountCaptionLabel;
    private Label itemCountValueLabel;

    private Panel sourceStatusPanel;
    private Label sourceStatusCaptionLabel;
    private Label sourceStatusValueLabel;

    private Panel notesSectionPanel;
    private Label notesLabel;
    private TextBox notesTextBox;

    private Panel itemsSectionPanel;
    private Label historicalItemsLabel;
    private Label historicalNoticeLabel;

    private DataGridView historicalItemsDataGridView;
    private DataGridViewTextBoxColumn itemNameColumn;
    private DataGridViewTextBoxColumn itemCodeColumn;
    private DataGridViewTextBoxColumn brandColumn;
    private DataGridViewTextBoxColumn quantityColumn;
    private DataGridViewTextBoxColumn unitPriceColumn;
    private DataGridViewTextBoxColumn lineTotalColumn;
    private DataGridViewTextBoxColumn sourceStatusColumn;

    private Panel footerPanel;
    private Label snapshotNoticeLabel;
    private Button closeButton;
}
