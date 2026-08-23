namespace ExpenseTracker.WinForms.Features.Expenses.Views
{
    partial class ExpensesView
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
            Expenses = new Label();
            SuspendLayout();
            // 
            // Expenses
            // 
            Expenses.AutoSize = true;
            Expenses.Location = new Point(0, 0);
            Expenses.Name = "Expenses";
            Expenses.Size = new Size(69, 20);
            Expenses.TabIndex = 0;
            Expenses.Text = "Expenses";
            // 
            // ExpensesViews
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Expenses);
            Name = "ExpensesViews";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Expenses;
    }
}
