namespace ExpenseTracker.WinForms.Features.Settings.Models;

public sealed class RegionalPreset
{
    public RegionalPreset(
        string countryName,
        string cultureName,
        string timeZoneId)
    {
        CountryName = countryName;
        CultureName = cultureName;
        TimeZoneId = timeZoneId;
    }

    public string CountryName { get; }

    public string CultureName { get; }

    public string TimeZoneId { get; }

    public override string ToString()
    {
        return CountryName;
    }
}
