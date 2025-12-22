using System.Text.Json.Serialization;

namespace TestCaseApi.Models;

public class AiResponse
{
    [JsonPropertyName("model_used")]
    public string ModelUsed { get; set; } = string.Empty;

    [JsonPropertyName("metrics")]
    public Metrics? Metrics { get; set; }

    [JsonPropertyName("result")]
    public AiResult? Result { get; set; }

    [JsonPropertyName("test_case")]
    public TestCase TestCase { get; set; } = new();

    [JsonPropertyName("script")]
    public string Script { get; set; } = string.Empty;    
    
    [JsonPropertyName("script_code")]
    public string ScriptCode { get; set; } = string.Empty;
}
