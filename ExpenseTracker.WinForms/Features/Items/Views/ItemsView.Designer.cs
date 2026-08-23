namespace ExpenseTracker.WinForms.Features.Items.Views
{
    partial class ItemsView
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
            Items = new Label();
            SuspendLayout();
            // 
            // Items
            // 
            Items.AutoSize = true;
            Items.Location = new Point(0, 0);
            Items.Name = "Items";
            Items.Size = new Size(45, 20);
            Items.TabIndex = 0;
            Items.Text = "Items";
            // 
            // ItemsView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Items);
            Name = "ItemsView";
            Load += ItemsView_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Items;
    }
}
