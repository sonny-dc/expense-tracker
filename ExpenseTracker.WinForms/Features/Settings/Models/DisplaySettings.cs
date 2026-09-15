namespace ExpenseTracker.WinForms.Features.Settings.Models;

public sealed class DisplaySettings
{
    public string CountryName { get; set; } =
        RegionalPresets.Philippines.CountryName;
}
