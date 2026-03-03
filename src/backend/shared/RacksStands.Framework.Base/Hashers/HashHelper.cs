using System;
using System.Security.Cryptography;
using System.Text;

namespace RacksStands.Framework.Base.Hashers;

public static class HashHelper
{
    // =========================
    // STRING INPUT (UTF8)
    // =========================

    public static string MD5(string input) =>
        ComputeHash(input, System.Security.Cryptography.MD5.Create());

    public static string SHA1(string input) =>
        ComputeHash(input, System.Security.Cryptography.SHA1.Create());

    public static string SHA256(string input) =>
        ComputeHash(input, System.Security.Cryptography.SHA256.Create());

    public static string SHA384(string input) =>
        ComputeHash(input, System.Security.Cryptography.SHA384.Create());

    public static string SHA512(string input) =>
        ComputeHash(input, System.Security.Cryptography.SHA512.Create());


    // =========================
    // BYTE INPUT
    // =========================

    public static byte[] MD5(ReadOnlySpan<byte> data) =>
        ComputeHash(data, System.Security.Cryptography.MD5.Create());

    public static byte[] SHA1(ReadOnlySpan<byte> data) =>
        ComputeHash(data, System.Security.Cryptography.SHA1.Create());

    public static byte[] SHA256(ReadOnlySpan<byte> data) =>
        ComputeHash(data, System.Security.Cryptography.SHA256.Create());

    public static byte[] SHA384(ReadOnlySpan<byte> data) =>
        ComputeHash(data, System.Security.Cryptography.SHA384.Create());

    public static byte[] SHA512(ReadOnlySpan<byte> data) =>
        ComputeHash(data, System.Security.Cryptography.SHA512.Create());

    // ==============================
    // HEX CONVERSION
    // ==============================

    public static string ToHexString(ReadOnlySpan<byte> bytes, bool lowerCase = true)
    {
        var hex = Convert.ToHexString(bytes);
        return lowerCase ? hex.ToLowerInvariant() : hex;
    }


    // =========================
    // INTERNAL CORE METHODS
    // =========================

    private static string ComputeHash(string input, HashAlgorithm algorithm)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        using (algorithm)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = algorithm.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }

    private static byte[] ComputeHash(ReadOnlySpan<byte> data, HashAlgorithm algorithm)
    {
        using (algorithm)
        {
            return algorithm.ComputeHash(data.ToArray());
        }
    }
}
