using System;
using System.Text;

namespace RacksStands.Framework.Base.Encoders;
public static class Base64UrlEncoderHelper
{
    /// <summary>
    /// Encodes bytes into Base64Url format (RFC 4648, no padding).
    /// </summary>
    public static string Encode(ReadOnlySpan<byte> data)
    {
        string base64 = Convert.ToBase64String(data);

        return base64
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    /// <summary>
    /// Encodes string into Base64Url using UTF8.
    /// </summary>
    public static string Encode(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        return Encode(Encoding.UTF8.GetBytes(value));
    }

    /// <summary>
    /// Decodes Base64Url string into byte array.
    /// </summary>
    public static byte[] DecodeToBytes(string base64Url)
    {
        if (string.IsNullOrWhiteSpace(base64Url))
            throw new ArgumentNullException(nameof(base64Url));

        string padded = base64Url
            .Replace('-', '+')
            .Replace('_', '/');

        switch (padded.Length % 4)
        {
            case 2: padded += "=="; break;
            case 3: padded += "="; break;
        }

        return Convert.FromBase64String(padded);
    }

    /// <summary>
    /// Decodes Base64Url string into UTF8 string.
    /// </summary>
    public static string Decode(string base64Url)
    {
        var bytes = DecodeToBytes(base64Url);
        return Encoding.UTF8.GetString(bytes);
    }
}
