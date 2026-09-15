using System.Globalization;

namespace ExpenseTracker.WinForms.Features.Settings.Models;

public static class RegionalPresets
{
    public static RegionalPreset Philippines { get; } =
        new(
            countryName: "Philippines",
            cultureName: "en-PH",
            timeZoneId: "Singapore Standard Time");

    public static IReadOnlyList<RegionalPreset> All { get; } =
    [
        Philippines,

        new RegionalPreset(
            countryName: "United States",
            cultureName: "en-US",
            timeZoneId: "Eastern Standard Time"),

        new RegionalPreset(
            countryName: "Germany",
            cultureName: "de-DE",
            timeZoneId: "W. Europe Standard Time"),

        new RegionalPreset(
            countryName: "France",
            cultureName: "fr-FR",
            timeZoneId: "Romance Standard Time"),

        new RegionalPreset(
            countryName: "United Kingdom",
            cultureName: "en-GB",
            timeZoneId: "GMT Standard Time"),

        new RegionalPreset(
            countryName: "China",
            cultureName: "zh-CN",
            timeZoneId: "China Standard Time"),

        new RegionalPreset(
            countryName: "Japan",
            cultureName: "ja-JP",
            timeZoneId: "Tokyo Standard Time"),

        new RegionalPreset(
            countryName: "India",
            cultureName: "en-IN",
            timeZoneId: "India Standard Time"),

        new RegionalPreset(
            countryName: "United Arab Emirates",
            cultureName: "en-AE",
            timeZoneId: "Arabian Standard Time"),

        new RegionalPreset(
            countryName: "Hong Kong",
            cultureName: "en-HK",
            timeZoneId: "China Standard Time"),

        new RegionalPreset(
            countryName: "Vietnam",
            cultureName: "vi-VN",
            timeZoneId: "SE Asia Standard Time"),

        new RegionalPreset(
            countryName: "South Korea",
            cultureName: "ko-KR",
            timeZoneId: "Korea Standard Time"),

        new RegionalPreset(
            countryName: "Russia",
            cultureName: "ru-RU",
            timeZoneId: "Russian Standard Time")
    ];

    public static RegionalPreset GetByCountryName(
        string countryName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(
            countryName);

        return All.FirstOrDefault(
                preset =>
                    string.Equals(
                        preset.CountryName,
                        countryName,
                        StringComparison.OrdinalIgnoreCase))
            ?? Philippines;
    }

    public static void Validate()
    {
        foreach (RegionalPreset preset in All)
        {
            _ = CultureInfo.GetCultureInfo(
                preset.CultureName);

            _ = TimeZoneInfo.FindSystemTimeZoneById(
                preset.TimeZoneId);
        }
    }
}
