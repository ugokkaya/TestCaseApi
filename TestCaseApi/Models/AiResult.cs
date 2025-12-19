using System.Text.Json.Serialization;

namespace TestCaseApi.Models;

public class AiResult
{
    [JsonPropertyName("raw_output")]
    public string RawOutput { get; set; } = string.Empty;

    [JsonPropertyName("test_case")]
    public TestCase? TestCase { get; set; }

    [JsonPropertyName("script")]
    public string Script { get; set; } = string.Empty;

    [JsonPropertyName("script_code")]
    public string ScriptCode { get; set; } = string.Empty;
}
