using System.Text.Json;

namespace RacksStands.Framework.Base.Extensions;

public static class StringExtensions
{
    public static string SanitizeForLogging(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Remove or mask sensitive information
        return input
            .Replace("password", "***")
            .Replace("token", "***")
            .Replace("secret", "***");
    }
}

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, DefaultOptions);
    }

    public static T? FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }
}
