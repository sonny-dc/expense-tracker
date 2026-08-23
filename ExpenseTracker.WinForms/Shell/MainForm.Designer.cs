namespace ExpenseTracker.WinForms.Shell;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        navigationPanel = new Panel();
        navigationMenuPanel = new Panel();
        expensesButton = new Button();
        itemsButton = new Button();
        homeButton = new Button();
        navigationHeaderPanel = new Panel();
        appTitleLabel = new Label();
        contentPanel = new Panel();
        navigationPanel.SuspendLayout();
        navigationMenuPanel.SuspendLayout();
        navigationHeaderPanel.SuspendLayout();
        SuspendLayout();
        // 
        // navigationPanel
        // 
        navigationPanel.BackColor = Color.Green;
        navigationPanel.Controls.Add(navigationMenuPanel);
        navigationPanel.Controls.Add(navigationHeaderPanel);
        navigationPanel.Dock = DockStyle.Left;
        navigationPanel.Location = new Point(0, 0);
        navigationPanel.Name = "navigationPanel";
        navigationPanel.Size = new Size(240, 653);
        navigationPanel.TabIndex = 0;
        // 
        // navigationMenuPanel
        // 
        navigationMenuPanel.Controls.Add(expensesButton);
        navigationMenuPanel.Controls.Add(itemsButton);
        navigationMenuPanel.Controls.Add(homeButton);
        navigationMenuPanel.Dock = DockStyle.Fill;
        navigationMenuPanel.Location = new Point(0, 90);
        navigationMenuPanel.Name = "navigationMenuPanel";
        navigationMenuPanel.Size = new Size(240, 563);
        navigationMenuPanel.TabIndex = 5;
        // 
        // expensesButton
        // 
        expensesButton.Cursor = Cursors.Hand;
        expensesButton.Dock = DockStyle.Top;
        expensesButton.FlatAppearance.BorderSize = 0;
        expensesButton.FlatStyle = FlatStyle.Flat;
        expensesButton.ForeColor = Color.White;
        expensesButton.Location = new Point(0, 102);
        expensesButton.Margin = new Padding(0);
        expensesButton.Name = "expensesButton";
        expensesButton.Padding = new Padding(24, 0, 0, 0);
        expensesButton.Size = new Size(240, 50);
        expensesButton.TabIndex = 5;
        expensesButton.TabStop = false;
        expensesButton.Text = "Expenses";
        expensesButton.TextAlign = ContentAlignment.MiddleLeft;
        expensesButton.UseVisualStyleBackColor = false;
        expensesButton.Click += expensesButton_Click;
        // 
        // itemsButton
        // 
        itemsButton.Cursor = Cursors.Hand;
        itemsButton.Dock = DockStyle.Top;
        itemsButton.FlatAppearance.BorderSize = 0;
        itemsButton.FlatStyle = FlatStyle.Flat;
        itemsButton.ForeColor = Color.White;
        itemsButton.Location = new Point(0, 52);
        itemsButton.Margin = new Padding(0);
        itemsButton.Name = "itemsButton";
        itemsButton.Padding = new Padding(24, 0, 0, 0);
        itemsButton.Size = new Size(240, 50);
        itemsButton.TabIndex = 4;
        itemsButton.TabStop = false;
        itemsButton.Text = "Items";
        itemsButton.TextAlign = ContentAlignment.MiddleLeft;
        itemsButton.UseVisualStyleBackColor = false;
        itemsButton.Click += itemsButton_Click;
        // 
        // homeButton
        // 
        homeButton.Cursor = Cursors.Hand;
        homeButton.Dock = DockStyle.Top;
        homeButton.FlatAppearance.BorderSize = 0;
        homeButton.FlatStyle = FlatStyle.Flat;
        homeButton.ForeColor = Color.White;
        homeButton.Location = new Point(0, 0);
        homeButton.Margin = new Padding(0);
        homeButton.Name = "homeButton";
        homeButton.Padding = new Padding(24, 0, 0, 0);
        homeButton.Size = new Size(240, 52);
        homeButton.TabIndex = 1;
        homeButton.TabStop = false;
        homeButton.Text = "Home";
        homeButton.TextAlign = ContentAlignment.MiddleLeft;
        homeButton.UseVisualStyleBackColor = false;
        homeButton.Click += homeButton_Click;
        // 
        // navigationHeaderPanel
        // 
        navigationHeaderPanel.Controls.Add(appTitleLabel);
        navigationHeaderPanel.Dock = DockStyle.Top;
        navigationHeaderPanel.Location = new Point(0, 0);
        navigationHeaderPanel.Name = "navigationHeaderPanel";
        navigationHeaderPanel.Size = new Size(240, 90);
        navigationHeaderPanel.TabIndex = 4;
        // 
        // appTitleLabel
        // 
        appTitleLabel.Dock = DockStyle.Fill;
        appTitleLabel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
        appTitleLabel.ForeColor = Color.White;
        appTitleLabel.Location = new Point(0, 0);
        appTitleLabel.Name = "appTitleLabel";
        appTitleLabel.Size = new Size(240, 90);
        appTitleLabel.TabIndex = 0;
        appTitleLabel.Text = "ExpenseTracker";
        appTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // contentPanel
        // 
        contentPanel.BackColor = Color.FromArgb(245, 247, 250);
        contentPanel.Dock = DockStyle.Fill;
        contentPanel.Location = new Point(240, 0);
        contentPanel.Name = "contentPanel";
        contentPanel.Size = new Size(842, 653);
        contentPanel.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1082, 653);
        Controls.Add(contentPanel);
        Controls.Add(navigationPanel);
        MinimumSize = new Size(900, 600);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ExpenseTracker";
        navigationPanel.ResumeLayout(false);
        navigationMenuPanel.ResumeLayout(false);
        navigationHeaderPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel navigationPanel;
    private Panel contentPanel;
    private Label appTitleLabel;
    private Button homeButton;
    private Panel navigationHeaderPanel;
    private Panel navigationMenuPanel;
    private Button expensesButton;
    private Button itemsButton;
}
