using RacksStands.Framework.Base.Encoders;
using System;

namespace RacksStands.Framework.Base.IdGenerators;

public static class IdGenerator
{
    // ==============================
    // GUID V4 (Random)
    // ==============================

    public static Guid NewGuid() => Guid.NewGuid();

    public static string NewGuidString() => Guid.NewGuid().ToString("N");


    // ==============================
    // GUID V7 (Time-Ordered, .NET 8+)
    // ==============================

#if NET8_0_OR_GREATER
    public static Guid NewGuidV7() => Guid.CreateVersion7();

    public static string NewGuidV7String() => Guid.CreateVersion7().ToString("N");
#endif


    // ==============================
    // Sequential GUID (SQL Friendly)
    // ==============================

    public static Guid NewSequentialGuid()
    {
        var guidBytes = Guid.NewGuid().ToByteArray();

        var timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

        // Overwrite last 6 bytes with timestamp
        guidBytes[10] = timestamp[2];
        guidBytes[11] = timestamp[3];
        guidBytes[12] = timestamp[4];
        guidBytes[13] = timestamp[5];
        guidBytes[14] = timestamp[6];
        guidBytes[15] = timestamp[7];

        return new Guid(guidBytes);
    }


    // ==============================
    // SHORT ID (Base64Url GUID)
    // ==============================

    public static string NewShortId()
    {
        var bytes = Guid.NewGuid().ToByteArray();
        return Base64UrlEncoderHelper.Encode(bytes);
    }


    // ==============================
    // Base64Url GUID (22 chars)
    // ==============================

    public static string NewCompactGuid()
    {
        return Base64UrlEncoderHelper.Encode(Guid.NewGuid().ToByteArray());
    }
}
