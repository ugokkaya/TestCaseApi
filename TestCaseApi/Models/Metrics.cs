using System.Text.Json.Serialization;

namespace TestCaseApi.Models;

public class Metrics
{
    [JsonPropertyName("latency_ms")]
    public double LatencyMs { get; set; }

    [JsonPropertyName("prompt_tokens")]
    public int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }

    [JsonPropertyName("total_duration_ms")]
    public double TotalDurationMs { get; set; }

    [JsonPropertyName("prompt_eval_duration_ms")]
    public double PromptEvalDurationMs { get; set; }

    [JsonPropertyName("eval_duration_ms")]
    public double EvalDurationMs { get; set; }
}
