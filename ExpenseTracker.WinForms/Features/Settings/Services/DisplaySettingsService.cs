using ExpenseTracker.WinForms.Features.Settings.Models;

namespace ExpenseTracker.WinForms.Features.Settings.Services;

public sealed class DisplaySettingsService
{
    private DisplaySettings _settings = new();

    public event EventHandler? SettingsChanged;

    public DisplaySettings Current =>
        _settings;

    public RegionalPreset CurrentPreset =>
        RegionalPresets.GetByCountryName(
            _settings.CountryName);

    public void UpdateCountry(
        string countryName)
    {
        RegionalPreset preset =
            RegionalPresets.GetByCountryName(
                countryName);

        if (string.Equals(
                _settings.CountryName,
                preset.CountryName,
                StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        _settings = new DisplaySettings
        {
            CountryName = preset.CountryName
        };

        SettingsChanged?.Invoke(
            this,
            EventArgs.Empty);
    }

    public void Reset()
    {
        if (string.Equals(
                _settings.CountryName,
                RegionalPresets.Philippines.CountryName,
                StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        _settings = new DisplaySettings();

        SettingsChanged?.Invoke(
            this,
            EventArgs.Empty);
    }
}
