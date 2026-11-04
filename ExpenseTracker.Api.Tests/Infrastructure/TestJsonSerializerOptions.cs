using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpenseTracker.Api.Tests.Infrastructure;

internal static class TestJsonSerializerOptions
{
    public static JsonSerializerOptions Default { get; } =
        new(JsonSerializerDefaults.Web)
        {
            UnmappedMemberHandling =
                JsonUnmappedMemberHandling.Disallow,
            Converters =
            {
                new JsonStringEnumConverter(
                    allowIntegerValues: false)
            }
        };
}
