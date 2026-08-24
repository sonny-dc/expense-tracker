namespace ExpenseTracker.WinForms.Features.Items.Views;

partial class ItemsView
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
        addItemButton = new Button();
        refreshButton = new Button();
        editItemButton = new Button();
        deleteItemButton = new Button();
        searchTextBox = new TextBox();
        searchLabel = new Label();
        itemsDataGridView = new DataGridView();
        nameColumn = new DataGridViewTextBoxColumn();
        codeColumn = new DataGridViewTextBoxColumn();
        brandColumn = new DataGridViewTextBoxColumn();
        unitPriceColumn = new DataGridViewTextBoxColumn();
        statusLabel = new Label();
        pageLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        toolbarPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)itemsDataGridView).BeginInit();
        SuspendLayout();
        // 
        // pageLayoutPanel
        // 
        pageLayoutPanel.ColumnCount = 1;
        pageLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100F));
        pageLayoutPanel.Controls.Add(headingPanel, 0, 0);
        pageLayoutPanel.Controls.Add(toolbarPanel, 0, 1);
        pageLayoutPanel.Controls.Add(itemsDataGridView, 0, 2);
        pageLayoutPanel.Controls.Add(statusLabel, 0, 3);
        pageLayoutPanel.Dock = DockStyle.Fill;
        pageLayoutPanel.Location = new Point(0, 0);
        pageLayoutPanel.Name = "pageLayoutPanel";
        pageLayoutPanel.Padding = new Padding(24);
        pageLayoutPanel.RowCount = 4;
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 82F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 70F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 36F));
        pageLayoutPanel.Size = new Size(842, 653);
        pageLayoutPanel.TabIndex = 0;
        // 
        // headingPanel
        // 
        headingPanel.Controls.Add(pageDescriptionLabel);
        headingPanel.Controls.Add(pageTitleLabel);
        headingPanel.Dock = DockStyle.Fill;
        headingPanel.Location = new Point(27, 27);
        headingPanel.Name = "headingPanel";
        headingPanel.Size = new Size(788, 76);
        headingPanel.TabIndex = 0;
        // 
        // pageDescriptionLabel
        // 
        pageDescriptionLabel.AutoSize = true;
        pageDescriptionLabel.ForeColor = Color.DimGray;
        pageDescriptionLabel.Location = new Point(0, 43);
        pageDescriptionLabel.Name = "pageDescriptionLabel";
        pageDescriptionLabel.Size = new Size(307, 20);
        pageDescriptionLabel.TabIndex = 1;
        pageDescriptionLabel.Text =
            "Manage the reusable items used in expenses.";
        // 
        // pageTitleLabel
        // 
        pageTitleLabel.AutoSize = true;
        pageTitleLabel.Font = new Font(
            "Segoe UI",
            20F,
            FontStyle.Bold);
        pageTitleLabel.ForeColor = Color.FromArgb(32, 40, 48);
        pageTitleLabel.Location = new Point(-3, -5);
        pageTitleLabel.Name = "pageTitleLabel";
        pageTitleLabel.Size = new Size(107, 46);
        pageTitleLabel.TabIndex = 0;
        pageTitleLabel.Text = "Items";
        // 
        // toolbarPanel
        // 
        toolbarPanel.Controls.Add(addItemButton);
        toolbarPanel.Controls.Add(refreshButton);
        toolbarPanel.Controls.Add(deleteItemButton);
        toolbarPanel.Controls.Add(editItemButton);
        toolbarPanel.Controls.Add(searchTextBox);
        toolbarPanel.Controls.Add(searchLabel);
        toolbarPanel.Dock = DockStyle.Fill;
        toolbarPanel.Location = new Point(27, 109);
        toolbarPanel.Name = "toolbarPanel";
        toolbarPanel.Size = new Size(788, 64);
        toolbarPanel.TabIndex = 1;
        // 
        // addItemButton
        // 
        addItemButton.Anchor =
            AnchorStyles.Top | AnchorStyles.Right;
        addItemButton.BackColor = Color.Green;
        addItemButton.Cursor = Cursors.Hand;
        addItemButton.FlatAppearance.BorderSize = 0;
        addItemButton.FlatStyle = FlatStyle.Flat;
        addItemButton.Font = new Font(
            "Segoe UI",
            9F,
            FontStyle.Bold);
        addItemButton.ForeColor = Color.White;
        addItemButton.Location = new Point(666, 18);
        addItemButton.Name = "addItemButton";
        addItemButton.Size = new Size(122, 38);
        addItemButton.TabIndex = 5;
        addItemButton.Text = "Add Item";
        addItemButton.UseVisualStyleBackColor = false;
        // 
        // editItemButton
        // 
        editItemButton.Anchor =
            AnchorStyles.Top | AnchorStyles.Right;
        editItemButton.Cursor = Cursors.Hand;
        editItemButton.Enabled = false;
        editItemButton.Location = new Point(354, 18);
        editItemButton.Name = "editItemButton";
        editItemButton.Size = new Size(94, 38);
        editItemButton.TabIndex = 2;
        editItemButton.Text = "Edit";
        editItemButton.UseVisualStyleBackColor = true;
        // 
        // deleteItemButton
        // 
        deleteItemButton.Anchor =
            AnchorStyles.Top | AnchorStyles.Right;
        deleteItemButton.Cursor = Cursors.Hand;
        deleteItemButton.Enabled = false;
        deleteItemButton.ForeColor = Color.Firebrick;
        deleteItemButton.Location = new Point(458, 18);
        deleteItemButton.Name = "deleteItemButton";
        deleteItemButton.Size = new Size(94, 38);
        deleteItemButton.TabIndex = 3;
        deleteItemButton.Text = "Delete";
        deleteItemButton.UseVisualStyleBackColor = true;
        // 
        // refreshButton
        // 
        refreshButton.Anchor =
            AnchorStyles.Top | AnchorStyles.Right;
        refreshButton.Cursor = Cursors.Hand;
        refreshButton.Location = new Point(562, 18);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(94, 38);
        refreshButton.TabIndex = 4;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new Point(0, 28);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText =
            "Search by name, code, or brand";
        searchTextBox.Size = new Size(330, 27);
        searchTextBox.TabIndex = 1;
        // 
        // searchLabel
        // 
        searchLabel.AutoSize = true;
        searchLabel.ForeColor = Color.FromArgb(64, 64, 64);
        searchLabel.Location = new Point(0, 4);
        searchLabel.Name = "searchLabel";
        searchLabel.Size = new Size(53, 20);
        searchLabel.TabIndex = 0;
        searchLabel.Text = "Search";
        // 
        // itemsDataGridView
        // 
        itemsDataGridView.AllowUserToAddRows = false;
        itemsDataGridView.AllowUserToDeleteRows = false;
        itemsDataGridView.AllowUserToResizeRows = false;
        itemsDataGridView.AutoGenerateColumns = false;
        itemsDataGridView.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        itemsDataGridView.BackgroundColor = Color.White;
        itemsDataGridView.BorderStyle = BorderStyle.Fixed3D;
        itemsDataGridView.ColumnHeadersHeight = 42;
        itemsDataGridView.ColumnHeadersHeightSizeMode =
            DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        itemsDataGridView.Columns.AddRange(
            nameColumn,
            codeColumn,
            brandColumn,
            unitPriceColumn);
        itemsDataGridView.Dock = DockStyle.Fill;
        itemsDataGridView.Location = new Point(27, 179);
        itemsDataGridView.MultiSelect = false;
        itemsDataGridView.Name = "itemsDataGridView";
        itemsDataGridView.ReadOnly = true;
        itemsDataGridView.RowHeadersVisible = false;
        itemsDataGridView.RowHeadersWidth = 51;
        itemsDataGridView.RowTemplate.Height = 38;
        itemsDataGridView.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        itemsDataGridView.Size = new Size(788, 411);
        itemsDataGridView.TabIndex = 2;
        // 
        // nameColumn
        // 
        nameColumn.DataPropertyName = "Name";
        nameColumn.FillWeight = 120F;
        nameColumn.HeaderText = "Name";
        nameColumn.MinimumWidth = 6;
        nameColumn.Name = "nameColumn";
        nameColumn.ReadOnly = true;
        // 
        // codeColumn
        // 
        codeColumn.DataPropertyName = "Code";
        codeColumn.HeaderText = "Code";
        codeColumn.MinimumWidth = 6;
        codeColumn.Name = "codeColumn";
        codeColumn.ReadOnly = true;
        // 
        // brandColumn
        // 
        brandColumn.DataPropertyName = "Brand";
        brandColumn.HeaderText = "Brand";
        brandColumn.MinimumWidth = 6;
        brandColumn.Name = "brandColumn";
        brandColumn.ReadOnly = true;
        // 
        // unitPriceColumn
        // 
        unitPriceColumn.DataPropertyName = "UnitPrice";
        unitPriceColumn.FillWeight = 115F;
        unitPriceColumn.HeaderText = "Unit Price";
        unitPriceColumn.MinimumWidth = 6;
        unitPriceColumn.Name = "unitPriceColumn";
        unitPriceColumn.ReadOnly = true;
        unitPriceColumn.DefaultCellStyle =
            new DataGridViewCellStyle
            {
                Alignment =
                    DataGridViewContentAlignment.MiddleRight
            };
        // 
        // statusLabel
        // 
        statusLabel.Dock = DockStyle.Fill;
        statusLabel.ForeColor = Color.DimGray;
        statusLabel.Location = new Point(27, 593);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(788, 36);
        statusLabel.TabIndex = 3;
        statusLabel.Text = "Ready";
        statusLabel.TextAlign =
            ContentAlignment.MiddleLeft;
        // 
        // ItemsView
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        Controls.Add(pageLayoutPanel);
        Name = "ItemsView";
        Size = new Size(842, 653);
        pageLayoutPanel.ResumeLayout(false);
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        toolbarPanel.ResumeLayout(false);
        toolbarPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)itemsDataGridView)
            .EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;
    private Panel headingPanel;
    private Label pageTitleLabel;
    private Label pageDescriptionLabel;
    private Panel toolbarPanel;
    private Label searchLabel;
    private TextBox searchTextBox;
    private Button editItemButton;
    private Button deleteItemButton;
    private Button refreshButton;
    private Button addItemButton;
    private DataGridView itemsDataGridView;
    private DataGridViewTextBoxColumn nameColumn;
    private DataGridViewTextBoxColumn codeColumn;
    private DataGridViewTextBoxColumn brandColumn;
    private DataGridViewTextBoxColumn unitPriceColumn;
    private Label statusLabel;
}
