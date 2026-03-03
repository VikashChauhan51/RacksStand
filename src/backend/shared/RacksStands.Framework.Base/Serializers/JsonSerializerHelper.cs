using System.Text.Json;
using System.Text.Json.Serialization;

namespace RacksStands.Framework.Base.Serializers;

public static class JsonSerializerHelper
{
    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
        }
    };

    public static T Deserialize<T>(ReadOnlySpan<byte> data) =>
        JsonSerializer.Deserialize<T>(data, Options);

    public static byte[] Serialize<T>(T obj) =>
        JsonSerializer.SerializeToUtf8Bytes(obj, Options);

    public static T Deserialize<T>(ReadOnlySpan<byte> data, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<T>(data, options);

    public static byte[] Serialize<T>(T obj, JsonSerializerOptions options) =>
        JsonSerializer.SerializeToUtf8Bytes(obj, options);

    public static T Deserialize<T>(string data) =>
      JsonSerializer.Deserialize<T>(data, Options);

    public static string SerializeString<T>(T obj) =>
        JsonSerializer.Serialize(obj, Options);

    public static T Deserialize<T>(string data, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<T>(data, options);

    public static string SerializeString<T>(T obj, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(obj, options);
}
