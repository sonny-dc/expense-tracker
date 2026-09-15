namespace ExpenseTracker.WinForms.Features.Items.Views;

partial class ItemEditorForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components =
            new System.ComponentModel.Container();

        formLayoutPanel = new TableLayoutPanel();
        headingPanel = new Panel();
        descriptionLabel = new Label();
        titleLabel = new Label();
        nameLabel = new Label();
        nameTextBox = new TextBox();
        codeLabel = new Label();
        codeTextBox = new TextBox();
        brandLabel = new Label();
        brandTextBox = new TextBox();
        unitPriceLabel = new Label();
        unitPriceNumericUpDown = new NumericUpDown();
        buttonPanel = new FlowLayoutPanel();
        saveButton = new Button();
        cancelButton = new Button();
        errorProvider = new ErrorProvider(components);

        formLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)
            unitPriceNumericUpDown).BeginInit();
        buttonPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)
            errorProvider).BeginInit();
        SuspendLayout();

        //
        // formLayoutPanel
        //
        formLayoutPanel.ColumnCount = 1;
        formLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 100F));
        formLayoutPanel.Controls.Add(headingPanel, 0, 0);
        formLayoutPanel.Controls.Add(nameLabel, 0, 1);
        formLayoutPanel.Controls.Add(nameTextBox, 0, 2);
        formLayoutPanel.Controls.Add(codeLabel, 0, 3);
        formLayoutPanel.Controls.Add(codeTextBox, 0, 4);
        formLayoutPanel.Controls.Add(brandLabel, 0, 5);
        formLayoutPanel.Controls.Add(brandTextBox, 0, 6);
        formLayoutPanel.Controls.Add(unitPriceLabel, 0, 7);
        formLayoutPanel.Controls.Add(
            unitPriceNumericUpDown,
            0,
            8);
        formLayoutPanel.Controls.Add(buttonPanel, 0, 9);
        formLayoutPanel.Dock = DockStyle.Fill;
        formLayoutPanel.Location = new Point(0, 0);
        formLayoutPanel.Name = "formLayoutPanel";
        formLayoutPanel.Padding = new Padding(24);
        formLayoutPanel.RowCount = 10;
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 78F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 28F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 42F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 28F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 42F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 28F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 42F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 28F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 42F));
        formLayoutPanel.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100F));
        formLayoutPanel.Size = new Size(500, 500);
        formLayoutPanel.TabIndex = 0;

        //
        // headingPanel
        //
        headingPanel.Controls.Add(descriptionLabel);
        headingPanel.Controls.Add(titleLabel);
        headingPanel.Dock = DockStyle.Fill;
        headingPanel.Location = new Point(27, 27);
        headingPanel.Name = "headingPanel";
        headingPanel.Size = new Size(446, 72);
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
        titleLabel.Location = new Point(-3, -5);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(143, 41);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Add Item";

        //
        // descriptionLabel
        //
        descriptionLabel.AutoSize = true;
        descriptionLabel.ForeColor = Color.DimGray;
        descriptionLabel.Location = new Point(0, 40);
        descriptionLabel.Name = "descriptionLabel";
        descriptionLabel.Size = new Size(275, 20);
        descriptionLabel.TabIndex = 1;
        descriptionLabel.Text =
            "Enter the details of the reusable item.";

        //
        // nameLabel
        //
        nameLabel.AutoSize = true;
        nameLabel.Dock = DockStyle.Fill;
        nameLabel.ForeColor = Color.FromArgb(64, 64, 64);
        nameLabel.Location = new Point(27, 102);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(446, 28);
        nameLabel.TabIndex = 1;
        nameLabel.Text = "Name";
        nameLabel.TextAlign =
            ContentAlignment.BottomLeft;

        //
        // nameTextBox
        //
        nameTextBox.Dock = DockStyle.Top;
        nameTextBox.Location = new Point(27, 133);
        nameTextBox.MaxLength = 100;
        nameTextBox.Name = "nameTextBox";
        nameTextBox.PlaceholderText = "Example: Bond Paper";
        nameTextBox.Size = new Size(446, 27);
        nameTextBox.TabIndex = 2;

        //
        // codeLabel
        //
        codeLabel.AutoSize = true;
        codeLabel.Dock = DockStyle.Fill;
        codeLabel.ForeColor = Color.FromArgb(64, 64, 64);
        codeLabel.Location = new Point(27, 172);
        codeLabel.Name = "codeLabel";
        codeLabel.Size = new Size(446, 28);
        codeLabel.TabIndex = 3;
        codeLabel.Text = "Code";
        codeLabel.TextAlign =
            ContentAlignment.BottomLeft;

        //
        // codeTextBox
        //
        codeTextBox.Dock = DockStyle.Top;
        codeTextBox.Location = new Point(27, 203);
        codeTextBox.MaxLength = 30;
        codeTextBox.Name = "codeTextBox";
        codeTextBox.PlaceholderText = "Example: BP-A4";
        codeTextBox.Size = new Size(446, 27);
        codeTextBox.TabIndex = 4;

        //
        // brandLabel
        //
        brandLabel.AutoSize = true;
        brandLabel.Dock = DockStyle.Fill;
        brandLabel.ForeColor = Color.FromArgb(64, 64, 64);
        brandLabel.Location = new Point(27, 242);
        brandLabel.Name = "brandLabel";
        brandLabel.Size = new Size(446, 28);
        brandLabel.TabIndex = 5;
        brandLabel.Text = "Brand";
        brandLabel.TextAlign =
            ContentAlignment.BottomLeft;

        //
        // brandTextBox
        //
        brandTextBox.Dock = DockStyle.Top;
        brandTextBox.Location = new Point(27, 273);
        brandTextBox.MaxLength = 100;
        brandTextBox.Name = "brandTextBox";
        brandTextBox.PlaceholderText = "Example: PaperOne";
        brandTextBox.Size = new Size(446, 27);
        brandTextBox.TabIndex = 6;

        //
        // unitPriceLabel
        //
        unitPriceLabel.AutoSize = true;
        unitPriceLabel.Dock = DockStyle.Fill;
        unitPriceLabel.ForeColor =
            Color.FromArgb(64, 64, 64);
        unitPriceLabel.Location = new Point(27, 312);
        unitPriceLabel.Name = "unitPriceLabel";
        unitPriceLabel.Size = new Size(446, 28);
        unitPriceLabel.TabIndex = 7;
        unitPriceLabel.Text = "Unit Price";
        unitPriceLabel.TextAlign =
            ContentAlignment.BottomLeft;

        //
        // unitPriceNumericUpDown
        //
        unitPriceNumericUpDown.DecimalPlaces = 2;
        unitPriceNumericUpDown.Dock = DockStyle.Top;
        unitPriceNumericUpDown.Increment = 0.25M;
        unitPriceNumericUpDown.Location =
            new Point(27, 343);
        unitPriceNumericUpDown.Maximum =
            9999999999999999.99M;
        unitPriceNumericUpDown.Name =
            "unitPriceNumericUpDown";
        unitPriceNumericUpDown.Size =
            new Size(446, 27);
        unitPriceNumericUpDown.TabIndex = 8;
        unitPriceNumericUpDown.TextAlign =
            HorizontalAlignment.Right;
        unitPriceNumericUpDown.ThousandsSeparator =
            true;

        //
        // buttonPanel
        //
        buttonPanel.Controls.Add(saveButton);
        buttonPanel.Controls.Add(cancelButton);
        buttonPanel.Dock = DockStyle.Fill;
        buttonPanel.FlowDirection =
            FlowDirection.RightToLeft;
        buttonPanel.Location = new Point(27, 385);
        buttonPanel.Name = "buttonPanel";
        buttonPanel.Padding = new Padding(0, 24, 0, 0);
        buttonPanel.Size = new Size(446, 88);
        buttonPanel.TabIndex = 9;
        buttonPanel.WrapContents = false;

        //
        // saveButton
        //
        saveButton.BackColor = Color.Green;
        saveButton.Cursor = Cursors.Hand;
        saveButton.FlatAppearance.BorderSize = 0;
        saveButton.FlatStyle = FlatStyle.Flat;
        saveButton.Font = new Font(
            "Segoe UI",
            9F,
            FontStyle.Bold);
        saveButton.ForeColor = Color.White;
        saveButton.Location = new Point(326, 27);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(117, 40);
        saveButton.TabIndex = 0;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = false;
        saveButton.Click += saveButton_Click;

        //
        // cancelButton
        //
        cancelButton.Cursor = Cursors.Hand;
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(203, 27);
        cancelButton.Margin = new Padding(3, 3, 8, 3);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(112, 40);
        cancelButton.TabIndex = 1;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;

        //
        // errorProvider
        //
        errorProvider.BlinkStyle =
            ErrorBlinkStyle.NeverBlink;
        errorProvider.ContainerControl = this;

        //
        // ItemEditorForm
        //
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        CancelButton = cancelButton;
        ClientSize = new Size(500, 500);
        Controls.Add(formLayoutPanel);
        FormBorderStyle =
            FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ItemEditorForm";
        ShowInTaskbar = false;
        StartPosition =
            FormStartPosition.CenterParent;
        Text = "Add Item";

        formLayoutPanel.ResumeLayout(false);
        formLayoutPanel.PerformLayout();
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)
            unitPriceNumericUpDown).EndInit();
        buttonPanel.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)
            errorProvider).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel formLayoutPanel;
    private Panel headingPanel;
    private Label titleLabel;
    private Label descriptionLabel;
    private Label nameLabel;
    private TextBox nameTextBox;
    private Label codeLabel;
    private TextBox codeTextBox;
    private Label brandLabel;
    private TextBox brandTextBox;
    private Label unitPriceLabel;
    private NumericUpDown unitPriceNumericUpDown;
    private FlowLayoutPanel buttonPanel;
    private Button saveButton;
    private Button cancelButton;
    private ErrorProvider errorProvider;
}
