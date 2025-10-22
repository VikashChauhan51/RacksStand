using System.Text;

namespace RacksStand.Api.Core.Logging;

public static class LogSanitizer
{
    /// <summary>
    /// Sanitizes a log value by removing special characters and masking the middle portion.
    /// </summary>
    public static string SanitizeAndMask(string? value, int visibleChars = 3, string mask = "***")
    {
        if (string.IsNullOrWhiteSpace(value))
            return mask;

        var sb = new StringBuilder(value.Length);

        // 1. Sanitize in a single pass
        foreach (var ch in value)
        {
            if (char.IsLetterOrDigit(ch))
            {
                sb.Append(ch);
            }
        }

        // 2. Check if too short
        if (sb.Length <= visibleChars * 2)
            return mask;

        var result = new StringBuilder(visibleChars * 2 + mask.Length);
        result.Append(sb.ToString(0, visibleChars));
        result.Append(mask);
        result.Append(sb.ToString(sb.Length - visibleChars, visibleChars));
        return result.ToString();
    }

}

