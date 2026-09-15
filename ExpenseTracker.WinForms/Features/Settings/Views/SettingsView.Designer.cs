namespace ExpenseTracker.WinForms.Features.Settings.Views;

partial class SettingsView
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
        pageLayoutPanel = new TableLayoutPanel();
        headingPanel = new Panel();
        pageDescriptionLabel = new Label();
        pageTitleLabel = new Label();

        settingsPanel = new Panel();
        settingsLayoutPanel = new TableLayoutPanel();

        countrySectionLabel = new Label();
        countryDescriptionLabel = new Label();
        countryLabel = new Label();
        countryComboBox = new ComboBox();

        previewSectionLabel = new Label();
        previewDescriptionLabel = new Label();

        previewTableLayoutPanel = new TableLayoutPanel();

        currencyPreviewPanel = new Panel();
        currencyCaptionLabel = new Label();
        currencyValueLabel = new Label();

        dateTimePreviewPanel = new Panel();
        dateTimeCaptionLabel = new Label();
        dateTimeValueLabel = new Label();

        technicalDetailsPanel = new Panel();
        technicalDetailsLabel = new Label();
        cultureCaptionLabel = new Label();
        cultureValueLabel = new Label();
        timeZoneCaptionLabel = new Label();
        timeZoneValueLabel = new Label();

        footerPanel = new Panel();
        statusLabel = new Label();
        resetButton = new Button();
        saveButton = new Button();

        pageLayoutPanel.SuspendLayout();
        headingPanel.SuspendLayout();
        settingsPanel.SuspendLayout();
        settingsLayoutPanel.SuspendLayout();
        previewTableLayoutPanel.SuspendLayout();
        currencyPreviewPanel.SuspendLayout();
        dateTimePreviewPanel.SuspendLayout();
        technicalDetailsPanel.SuspendLayout();
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
            headingPanel,
            0,
            0);
        pageLayoutPanel.Controls.Add(
            settingsPanel,
            0,
            1);
        pageLayoutPanel.Controls.Add(
            footerPanel,
            0,
            2);
        pageLayoutPanel.Dock = DockStyle.Fill;
        pageLayoutPanel.Location =
            new Point(0, 0);
        pageLayoutPanel.Name =
            "pageLayoutPanel";
        pageLayoutPanel.Padding =
            new Padding(24);
        pageLayoutPanel.RowCount = 3;
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                82F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));
        pageLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                76F));
        pageLayoutPanel.Size =
            new Size(842, 653);
        pageLayoutPanel.TabIndex = 0;

        // 
        // headingPanel
        // 
        headingPanel.Controls.Add(
            pageDescriptionLabel);
        headingPanel.Controls.Add(
            pageTitleLabel);
        headingPanel.Dock =
            DockStyle.Fill;
        headingPanel.Location =
            new Point(27, 27);
        headingPanel.Name =
            "headingPanel";
        headingPanel.Size =
            new Size(788, 76);
        headingPanel.TabIndex = 0;

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
            new Size(149, 46);
        pageTitleLabel.TabIndex = 0;
        pageTitleLabel.Text =
            "Settings";

        // 
        // pageDescriptionLabel
        // 
        pageDescriptionLabel.AutoSize =
            true;
        pageDescriptionLabel.ForeColor =
            Color.DimGray;
        pageDescriptionLabel.Location =
            new Point(0, 43);
        pageDescriptionLabel.Name =
            "pageDescriptionLabel";
        pageDescriptionLabel.Size =
            new Size(451, 20);
        pageDescriptionLabel.TabIndex = 1;
        pageDescriptionLabel.Text =
            "Customize displayed money and date/time values.";

        // 
        // settingsPanel
        // 
        settingsPanel.AutoScroll = true;
        settingsPanel.BackColor =
            Color.White;
        settingsPanel.BorderStyle =
            BorderStyle.FixedSingle;
        settingsPanel.Controls.Add(
            settingsLayoutPanel);
        settingsPanel.Dock =
            DockStyle.Fill;
        settingsPanel.Location =
            new Point(27, 109);
        settingsPanel.Name =
            "settingsPanel";
        settingsPanel.Padding =
            new Padding(24);
        settingsPanel.Size =
            new Size(788, 441);
        settingsPanel.TabIndex = 1;

        // 
        // settingsLayoutPanel
        // 
        settingsLayoutPanel.AutoSize =
            true;
        settingsLayoutPanel.AutoSizeMode =
            AutoSizeMode.GrowAndShrink;
        settingsLayoutPanel.ColumnCount = 1;
        settingsLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                100F));
        settingsLayoutPanel.Controls.Add(
            countrySectionLabel,
            0,
            0);
        settingsLayoutPanel.Controls.Add(
            countryDescriptionLabel,
            0,
            1);
        settingsLayoutPanel.Controls.Add(
            countryLabel,
            0,
            2);
        settingsLayoutPanel.Controls.Add(
            countryComboBox,
            0,
            3);
        settingsLayoutPanel.Controls.Add(
            previewSectionLabel,
            0,
            4);
        settingsLayoutPanel.Controls.Add(
            previewDescriptionLabel,
            0,
            5);
        settingsLayoutPanel.Controls.Add(
            previewTableLayoutPanel,
            0,
            6);
        settingsLayoutPanel.Controls.Add(
            technicalDetailsPanel,
            0,
            7);
        settingsLayoutPanel.Dock =
            DockStyle.Top;
        settingsLayoutPanel.Location =
            new Point(24, 24);
        settingsLayoutPanel.Name =
            "settingsLayoutPanel";
        settingsLayoutPanel.RowCount = 8;
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                34F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                42F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                28F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                54F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                42F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                38F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                116F));
        settingsLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Absolute,
                112F));
        settingsLayoutPanel.Size =
            new Size(738, 466);
        settingsLayoutPanel.TabIndex = 0;

        // 
        // countrySectionLabel
        // 
        countrySectionLabel.AutoSize =
            true;
        countrySectionLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);
        countrySectionLabel.Location =
            new Point(3, 0);
        countrySectionLabel.Name =
            "countrySectionLabel";
        countrySectionLabel.Size =
            new Size(159, 28);
        countrySectionLabel.TabIndex = 0;
        countrySectionLabel.Text =
            "Country Preset";

        // 
        // countryDescriptionLabel
        // 
        countryDescriptionLabel.AutoSize =
            true;
        countryDescriptionLabel.ForeColor =
            Color.DimGray;
        countryDescriptionLabel.Location =
            new Point(3, 34);
        countryDescriptionLabel.Name =
            "countryDescriptionLabel";
        countryDescriptionLabel.Size =
            new Size(627, 20);
        countryDescriptionLabel.TabIndex = 1;
        countryDescriptionLabel.Text =
            "The selected country controls currency and displayed date/time formatting.";

        // 
        // countryLabel
        // 
        countryLabel.AutoSize = true;
        countryLabel.Location =
            new Point(3, 76);
        countryLabel.Name =
            "countryLabel";
        countryLabel.Size =
            new Size(60, 20);
        countryLabel.TabIndex = 2;
        countryLabel.Text =
            "Country";

        // 
        // countryComboBox
        // 
        countryComboBox.Dock =
            DockStyle.Top;
        countryComboBox.DropDownStyle =
            ComboBoxStyle.DropDownList;
        countryComboBox.FormattingEnabled =
            true;
        countryComboBox.Location =
            new Point(3, 107);
        countryComboBox.Name =
            "countryComboBox";
        countryComboBox.Size =
            new Size(732, 28);
        countryComboBox.TabIndex = 3;

        // 
        // previewSectionLabel
        // 
        previewSectionLabel.AutoSize =
            true;
        previewSectionLabel.Font =
            new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold);
        previewSectionLabel.Location =
            new Point(3, 158);
        previewSectionLabel.Name =
            "previewSectionLabel";
        previewSectionLabel.Padding =
            new Padding(0, 8, 0, 0);
        previewSectionLabel.Size =
            new Size(93, 36);
        previewSectionLabel.TabIndex = 4;
        previewSectionLabel.Text =
            "Preview";

        // 
        // previewDescriptionLabel
        // 
        previewDescriptionLabel.AutoSize =
            true;
        previewDescriptionLabel.ForeColor =
            Color.DimGray;
        previewDescriptionLabel.Location =
            new Point(3, 200);
        previewDescriptionLabel.Name =
            "previewDescriptionLabel";
        previewDescriptionLabel.Size =
            new Size(454, 20);
        previewDescriptionLabel.TabIndex = 5;
        previewDescriptionLabel.Text =
            "Preview how values will appear throughout the application.";

        // 
        // previewTableLayoutPanel
        // 
        previewTableLayoutPanel.ColumnCount =
            2;
        previewTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                40F));
        previewTableLayoutPanel.ColumnStyles.Add(
            new ColumnStyle(
                SizeType.Percent,
                60F));
        previewTableLayoutPanel.Controls.Add(
            currencyPreviewPanel,
            0,
            0);
        previewTableLayoutPanel.Controls.Add(
            dateTimePreviewPanel,
            1,
            0);
        previewTableLayoutPanel.Dock =
            DockStyle.Fill;
        previewTableLayoutPanel.Location =
            new Point(3, 241);
        previewTableLayoutPanel.Name =
            "previewTableLayoutPanel";
        previewTableLayoutPanel.RowCount = 1;
        previewTableLayoutPanel.RowStyles.Add(
            new RowStyle(
                SizeType.Percent,
                100F));
        previewTableLayoutPanel.Size =
            new Size(732, 110);
        previewTableLayoutPanel.TabIndex = 6;

        // 
        // currencyPreviewPanel
        // 
        currencyPreviewPanel.BackColor =
            Color.FromArgb(247, 250, 248);
        currencyPreviewPanel.BorderStyle =
            BorderStyle.FixedSingle;
        currencyPreviewPanel.Controls.Add(
            currencyValueLabel);
        currencyPreviewPanel.Controls.Add(
            currencyCaptionLabel);
        currencyPreviewPanel.Dock =
            DockStyle.Fill;
        currencyPreviewPanel.Margin =
            new Padding(0, 4, 8, 4);
        currencyPreviewPanel.Name =
            "currencyPreviewPanel";
        currencyPreviewPanel.TabIndex = 0;

        // 
        // currencyCaptionLabel
        // 
        currencyCaptionLabel.AutoSize =
            true;
        currencyCaptionLabel.Font =
            new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);
        currencyCaptionLabel.ForeColor =
            Color.DimGray;
        currencyCaptionLabel.Location =
            new Point(16, 15);
        currencyCaptionLabel.Name =
            "currencyCaptionLabel";
        currencyCaptionLabel.Size =
            new Size(131, 20);
        currencyCaptionLabel.TabIndex = 0;
        currencyCaptionLabel.Text =
            "MONEY PREVIEW";

        // 
        // currencyValueLabel
        // 
        currencyValueLabel.AutoEllipsis =
            true;
        currencyValueLabel.Font =
            new Font(
                "Segoe UI",
                17F,
                FontStyle.Bold);
        currencyValueLabel.ForeColor =
            Color.Green;
        currencyValueLabel.Location =
            new Point(14, 44);
        currencyValueLabel.Name =
            "currencyValueLabel";
        currencyValueLabel.Size =
            new Size(250, 42);
        currencyValueLabel.TabIndex = 1;
        currencyValueLabel.Text =
            "₱1,234.50";

        // 
        // dateTimePreviewPanel
        // 
        dateTimePreviewPanel.BackColor =
            Color.FromArgb(247, 250, 248);
        dateTimePreviewPanel.BorderStyle =
            BorderStyle.FixedSingle;
        dateTimePreviewPanel.Controls.Add(
            dateTimeValueLabel);
        dateTimePreviewPanel.Controls.Add(
            dateTimeCaptionLabel);
        dateTimePreviewPanel.Dock =
            DockStyle.Fill;
        dateTimePreviewPanel.Margin =
            new Padding(8, 4, 0, 4);
        dateTimePreviewPanel.Name =
            "dateTimePreviewPanel";
        dateTimePreviewPanel.TabIndex = 1;

        // 
        // dateTimeCaptionLabel
        // 
        dateTimeCaptionLabel.AutoSize =
            true;
        dateTimeCaptionLabel.Font =
            new Font(
                "Segoe UI",
                8.5F,
                FontStyle.Bold);
        dateTimeCaptionLabel.ForeColor =
            Color.DimGray;
        dateTimeCaptionLabel.Location =
            new Point(16, 15);
        dateTimeCaptionLabel.Name =
            "dateTimeCaptionLabel";
        dateTimeCaptionLabel.Size =
            new Size(166, 20);
        dateTimeCaptionLabel.TabIndex = 0;
        dateTimeCaptionLabel.Text =
            "DATE/TIME PREVIEW";

        // 
        // dateTimeValueLabel
        // 
        dateTimeValueLabel.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Left |
            AnchorStyles.Right;
        dateTimeValueLabel.AutoEllipsis =
            true;
        dateTimeValueLabel.Font =
            new Font(
                "Segoe UI",
                13F,
                FontStyle.Bold);
        dateTimeValueLabel.ForeColor =
            Color.FromArgb(32, 40, 48);
        dateTimeValueLabel.Location =
            new Point(14, 48);
        dateTimeValueLabel.Name =
            "dateTimeValueLabel";
        dateTimeValueLabel.Size =
            new Size(392, 34);
        dateTimeValueLabel.TabIndex = 1;
        dateTimeValueLabel.Text =
            "August 24, 2026 3:00 PM";

        // 
        // technicalDetailsPanel
        // 
        technicalDetailsPanel.BackColor =
            Color.FromArgb(250, 250, 250);
        technicalDetailsPanel.BorderStyle =
            BorderStyle.FixedSingle;
        technicalDetailsPanel.Controls.Add(
            timeZoneValueLabel);
        technicalDetailsPanel.Controls.Add(
            timeZoneCaptionLabel);
        technicalDetailsPanel.Controls.Add(
            cultureValueLabel);
        technicalDetailsPanel.Controls.Add(
            cultureCaptionLabel);
        technicalDetailsPanel.Controls.Add(
            technicalDetailsLabel);
        technicalDetailsPanel.Dock =
            DockStyle.Fill;
        technicalDetailsPanel.Location =
            new Point(3, 357);
        technicalDetailsPanel.Name =
            "technicalDetailsPanel";
        technicalDetailsPanel.Size =
            new Size(732, 106);
        technicalDetailsPanel.TabIndex = 7;

        // 
        // technicalDetailsLabel
        // 
        technicalDetailsLabel.AutoSize =
            true;
        technicalDetailsLabel.Font =
            new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);
        technicalDetailsLabel.Location =
            new Point(15, 10);
        technicalDetailsLabel.Name =
            "technicalDetailsLabel";
        technicalDetailsLabel.Size =
            new Size(120, 20);
        technicalDetailsLabel.TabIndex = 0;
        technicalDetailsLabel.Text =
            "Preset Details";

        // 
        // cultureCaptionLabel
        // 
        cultureCaptionLabel.AutoSize =
            true;
        cultureCaptionLabel.ForeColor =
            Color.DimGray;
        cultureCaptionLabel.Location =
            new Point(16, 39);
        cultureCaptionLabel.Name =
            "cultureCaptionLabel";
        cultureCaptionLabel.Size =
            new Size(61, 20);
        cultureCaptionLabel.TabIndex = 1;
        cultureCaptionLabel.Text =
            "Culture:";

        // 
        // cultureValueLabel
        // 
        cultureValueLabel.AutoSize = true;
        cultureValueLabel.Location =
            new Point(130, 39);
        cultureValueLabel.Name =
            "cultureValueLabel";
        cultureValueLabel.Size =
            new Size(47, 20);
        cultureValueLabel.TabIndex = 2;
        cultureValueLabel.Text =
            "en-PH";

        // 
        // timeZoneCaptionLabel
        // 
        timeZoneCaptionLabel.AutoSize =
            true;
        timeZoneCaptionLabel.ForeColor =
            Color.DimGray;
        timeZoneCaptionLabel.Location =
            new Point(16, 70);
        timeZoneCaptionLabel.Name =
            "timeZoneCaptionLabel";
        timeZoneCaptionLabel.Size =
            new Size(78, 20);
        timeZoneCaptionLabel.TabIndex = 3;
        timeZoneCaptionLabel.Text =
            "Timezone:";

        // 
        // timeZoneValueLabel
        // 
        timeZoneValueLabel.AutoEllipsis =
            true;
        timeZoneValueLabel.Location =
            new Point(130, 70);
        timeZoneValueLabel.Name =
            "timeZoneValueLabel";
        timeZoneValueLabel.Size =
            new Size(570, 23);
        timeZoneValueLabel.TabIndex = 4;
        timeZoneValueLabel.Text =
            "Singapore Standard Time";

        // 
        // footerPanel
        // 
        footerPanel.Controls.Add(
            saveButton);
        footerPanel.Controls.Add(
            resetButton);
        footerPanel.Controls.Add(
            statusLabel);
        footerPanel.Dock = DockStyle.Fill;
        footerPanel.Location =
            new Point(27, 556);
        footerPanel.Name =
            "footerPanel";
        footerPanel.Size =
            new Size(788, 70);
        footerPanel.TabIndex = 2;

        // 
        // statusLabel
        // 
        statusLabel.Anchor =
            AnchorStyles.Left;
        statusLabel.AutoEllipsis = true;
        statusLabel.ForeColor =
            Color.DimGray;
        statusLabel.Location =
            new Point(0, 22);
        statusLabel.Name =
            "statusLabel";
        statusLabel.Size =
            new Size(440, 24);
        statusLabel.TabIndex = 0;
        statusLabel.Text =
            "Settings are ready.";

        // 
        // resetButton
        // 
        resetButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;
        resetButton.Cursor =
            Cursors.Hand;
        resetButton.Location =
            new Point(548, 15);
        resetButton.Name =
            "resetButton";
        resetButton.Size =
            new Size(108, 40);
        resetButton.TabIndex = 1;
        resetButton.Text =
            "Reset";
        resetButton.UseVisualStyleBackColor =
            true;

        // 
        // saveButton
        // 
        saveButton.Anchor =
            AnchorStyles.Top |
            AnchorStyles.Right;
        saveButton.BackColor =
            Color.Green;
        saveButton.Cursor =
            Cursors.Hand;
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
        saveButton.Location =
            new Point(666, 15);
        saveButton.Name =
            "saveButton";
        saveButton.Size =
            new Size(122, 40);
        saveButton.TabIndex = 2;
        saveButton.Text =
            "Save Settings";
        saveButton.UseVisualStyleBackColor =
            false;

        // 
        // SettingsView
        // 
        AutoScaleDimensions =
            new SizeF(8F, 20F);
        AutoScaleMode =
            AutoScaleMode.Font;
        BackColor =
            Color.FromArgb(245, 247, 250);
        Controls.Add(
            pageLayoutPanel);
        Name =
            "SettingsView";
        Size =
            new Size(842, 653);

        pageLayoutPanel.ResumeLayout(false);
        headingPanel.ResumeLayout(false);
        headingPanel.PerformLayout();
        settingsPanel.ResumeLayout(false);
        settingsPanel.PerformLayout();
        settingsLayoutPanel.ResumeLayout(false);
        settingsLayoutPanel.PerformLayout();
        previewTableLayoutPanel.ResumeLayout(false);
        currencyPreviewPanel.ResumeLayout(false);
        currencyPreviewPanel.PerformLayout();
        dateTimePreviewPanel.ResumeLayout(false);
        dateTimePreviewPanel.PerformLayout();
        technicalDetailsPanel.ResumeLayout(false);
        technicalDetailsPanel.PerformLayout();
        footerPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageLayoutPanel;
    private Panel headingPanel;
    private Label pageTitleLabel;
    private Label pageDescriptionLabel;

    private Panel settingsPanel;
    private TableLayoutPanel settingsLayoutPanel;

    private Label countrySectionLabel;
    private Label countryDescriptionLabel;
    private Label countryLabel;
    private ComboBox countryComboBox;

    private Label previewSectionLabel;
    private Label previewDescriptionLabel;
    private TableLayoutPanel previewTableLayoutPanel;

    private Panel currencyPreviewPanel;
    private Label currencyCaptionLabel;
    private Label currencyValueLabel;

    private Panel dateTimePreviewPanel;
    private Label dateTimeCaptionLabel;
    private Label dateTimeValueLabel;

    private Panel technicalDetailsPanel;
    private Label technicalDetailsLabel;
    private Label cultureCaptionLabel;
    private Label cultureValueLabel;
    private Label timeZoneCaptionLabel;
    private Label timeZoneValueLabel;

    private Panel footerPanel;
    private Label statusLabel;
    private Button resetButton;
    private Button saveButton;
}
