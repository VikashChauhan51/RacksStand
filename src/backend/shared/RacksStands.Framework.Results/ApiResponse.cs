using System.Text.Json.Serialization;

namespace RacksStands.Framework.Results;

public record ApiResponse
{

    [JsonPropertyName("success")]
    public bool Success { get; init; }
    public string Error { get; init; } = string.Empty;

    public IReadOnlyDictionary<string, object> ErrorMeta { get; init; } = new Dictionary<string, object>(StringComparer.Ordinal);

}


public record ApiResponse<T> : ApiResponse
{
    [JsonPropertyName("data")]
    public T? Data { get; init; }
}
