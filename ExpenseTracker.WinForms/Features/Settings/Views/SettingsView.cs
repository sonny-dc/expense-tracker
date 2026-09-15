using ExpenseTracker.WinForms.Features.Settings.Models;
using ExpenseTracker.WinForms.Features.Settings.Services;
using ExpenseTracker.WinForms.Infrastructure.Presentation;

namespace ExpenseTracker.WinForms.Features.Settings.Views;

public partial class SettingsView : UserControl
{
    private const decimal PreviewAmount = 1234.50m;

    private readonly DisplaySettingsService _settingsService;
    private readonly DisplayFormatter _displayFormatter;

    public SettingsView(
        DisplaySettingsService settingsService,
        DisplayFormatter displayFormatter)
    {
        InitializeComponent();

        _settingsService = settingsService;
        _displayFormatter = displayFormatter;

        ConfigureCountryPresets();
        SelectCurrentPreset();
        UpdatePreview();

        countryComboBox.SelectedIndexChanged +=
            countryComboBox_SelectedIndexChanged;

        saveButton.Click +=
            saveButton_Click;

        resetButton.Click +=
            resetButton_Click;
    }

    private void ConfigureCountryPresets()
    {
        countryComboBox.DisplayMember =
            nameof(RegionalPreset.CountryName);

        countryComboBox.DataSource =
            RegionalPresets.All.ToList();
    }

    private void SelectCurrentPreset()
    {
        RegionalPreset currentPreset =
            _settingsService.CurrentPreset;

        for (int index = 0;
            index < countryComboBox.Items.Count;
            index++)
        {
            if (countryComboBox.Items[index]
                is not RegionalPreset preset)
            {
                continue;
            }

            if (!string.Equals(
                    preset.CountryName,
                    currentPreset.CountryName,
                    StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            countryComboBox.SelectedIndex = index;

            return;
        }

        countryComboBox.SelectedIndex = 0;
    }

    private void countryComboBox_SelectedIndexChanged(
        object? sender,
        EventArgs e)
    {
        UpdatePreview();
        statusLabel.Text =
            "Review the preview, then save your changes.";
    }

    private void saveButton_Click(
        object? sender,
        EventArgs e)
    {
        RegionalPreset? selectedPreset =
            GetSelectedPreset();

        if (selectedPreset is null)
        {
            return;
        }

        _settingsService.UpdateCountry(
            selectedPreset.CountryName);

        UpdatePreview();

        statusLabel.Text =
            $"Settings saved for {selectedPreset.CountryName}.";

        MessageBox.Show(
            this,
            "The money and date/time display settings were saved.\n\n" +
            "Existing screens will use the new format when refreshed.",
            "Settings Saved",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void resetButton_Click(
        object? sender,
        EventArgs e)
    {
        DialogResult resetResult =
            MessageBox.Show(
                this,
                "Reset the country preset to Philippines?",
                "Reset Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

        if (resetResult != DialogResult.Yes)
        {
            return;
        }

        _settingsService.Reset();

        SelectCurrentPreset();
        UpdatePreview();

        statusLabel.Text =
            "Settings reset to Philippines.";
    }

    private void UpdatePreview()
    {
        RegionalPreset? selectedPreset =
            GetSelectedPreset();

        if (selectedPreset is null)
        {
            return;
        }

        currencyValueLabel.Text =
            _displayFormatter.FormatCurrency(
                PreviewAmount,
                selectedPreset);

        dateTimeValueLabel.Text =
            _displayFormatter.FormatUtcDateTimeLong(
                DateTime.UtcNow,
                selectedPreset);

        cultureValueLabel.Text =
            selectedPreset.CultureName;

        timeZoneValueLabel.Text =
            selectedPreset.TimeZoneId;
    }

    private RegionalPreset? GetSelectedPreset()
    {
        return countryComboBox.SelectedItem
            as RegionalPreset;
    }
}
