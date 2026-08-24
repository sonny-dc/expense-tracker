using System.Globalization;

using ExpenseTracker.WinForms.Features.Settings.Models;
using ExpenseTracker.WinForms.Features.Settings.Services;

namespace ExpenseTracker.WinForms.Infrastructure.Presentation;

public sealed class DisplayFormatter
{
    private readonly DisplaySettingsService _settingsService;

    public DisplayFormatter(
        DisplaySettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public string FormatCurrency(
        decimal amount)
    {
        CultureInfo culture =
            GetCurrentCulture();

        return amount.ToString(
            "C2",
            culture);
    }

    public string FormatUtcDateTime(
        DateTime dateTime)
    {
        DateTime utcDateTime =
            dateTime.Kind == DateTimeKind.Utc
                ? dateTime
                : DateTime.SpecifyKind(
                    dateTime,
                    DateTimeKind.Utc);

        TimeZoneInfo timeZone =
            GetCurrentTimeZone();

        DateTime displayDateTime =
            TimeZoneInfo.ConvertTimeFromUtc(
                utcDateTime,
                timeZone);

        CultureInfo culture =
            GetCurrentCulture();

        return displayDateTime.ToString(
            "g",
            culture);
    }

    public string FormatUtcDateTimeLong(
        DateTime dateTime)
    {
        DateTime utcDateTime =
            dateTime.Kind == DateTimeKind.Utc
                ? dateTime
                : DateTime.SpecifyKind(
                    dateTime,
                    DateTimeKind.Utc);

        TimeZoneInfo timeZone =
            GetCurrentTimeZone();

        DateTime displayDateTime =
            TimeZoneInfo.ConvertTimeFromUtc(
                utcDateTime,
                timeZone);

        CultureInfo culture =
            GetCurrentCulture();

        return displayDateTime.ToString(
            "f",
            culture);
    }

    public string FormatCurrentDateTimePreview()
    {
        DateTime utcNow =
            DateTime.UtcNow;

        return FormatUtcDateTimeLong(utcNow);
    }

    public string FormatCurrencyPreview()
    {
        const decimal previewAmount = 1234.50m;

        return FormatCurrency(previewAmount);
    }

    public RegionalPreset GetCurrentPreset()
    {
        return _settingsService.CurrentPreset;
    }

    private CultureInfo GetCurrentCulture()
    {
        RegionalPreset preset =
            _settingsService.CurrentPreset;

        return CultureInfo.GetCultureInfo(
            preset.CultureName);
    }

    private TimeZoneInfo GetCurrentTimeZone()
    {
        RegionalPreset preset =
            _settingsService.CurrentPreset;

        return TimeZoneInfo.FindSystemTimeZoneById(
            preset.TimeZoneId);
    }

    public string FormatCurrency(
        decimal amount,
        RegionalPreset preset)
    {
        ArgumentNullException.ThrowIfNull(preset);

        CultureInfo culture =
            CultureInfo.GetCultureInfo(
                preset.CultureName);

        return amount.ToString(
            "C2",
            culture);
    }

    public string FormatUtcDateTimeLong(
        DateTime dateTime,
        RegionalPreset preset)
    {
        ArgumentNullException.ThrowIfNull(preset);

        DateTime utcDateTime =
            dateTime.Kind == DateTimeKind.Utc
                ? dateTime
                : DateTime.SpecifyKind(
                    dateTime,
                    DateTimeKind.Utc);

        TimeZoneInfo timeZone =
            TimeZoneInfo.FindSystemTimeZoneById(
                preset.TimeZoneId);

        DateTime displayDateTime =
            TimeZoneInfo.ConvertTimeFromUtc(
                utcDateTime,
                timeZone);

        CultureInfo culture =
            CultureInfo.GetCultureInfo(
                preset.CultureName);

        return displayDateTime.ToString(
            "f",
            culture);
    }
}
