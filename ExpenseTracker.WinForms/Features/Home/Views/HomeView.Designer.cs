namespace ExpenseTracker.WinForms.Features.Home.Views;

partial class HomeView
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Home = new Label();
        SuspendLayout();
        // 
        // Home
        // 
        Home.AutoSize = true;
        Home.Location = new Point(0, 0);
        Home.Name = "Home";
        Home.Size = new Size(50, 20);
        Home.TabIndex = 0;
        Home.Text = "Home";
        // 
        // HomeView
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(Home);
        Name = "HomeView";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label Home;
}
