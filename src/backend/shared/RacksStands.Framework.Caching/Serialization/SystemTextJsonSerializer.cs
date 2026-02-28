using RacksStands.Framework.Caching.Abstractions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RacksStands.Framework.Caching.Serialization;

public sealed class SystemTextJsonSerializer : ISerializer
{
    private static readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
        }
    };

    public T Deserialize<T>(ReadOnlySpan<byte> data) =>
        JsonSerializer.Deserialize<T>(data, _options);

    public byte[] Serialize<T>(T obj) =>
        JsonSerializer.SerializeToUtf8Bytes(obj, _options);
}
