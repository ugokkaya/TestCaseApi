using System.Text.Json.Serialization;

namespace TestCaseApi.Models;

public class AiParsedPayload
{
    [JsonPropertyName("test_case")]
    public TestCase TestCase { get; set; } = new();

    [JsonPropertyName("script")]
    public string Script { get; set; } = string.Empty;

    [JsonPropertyName("script_code")]
    public string ScriptCode { get; set; } = string.Empty;
}
