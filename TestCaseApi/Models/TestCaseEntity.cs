using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCaseApi.Models;

[Table("test_cases")]
public class TestCaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("requirement")]
    public string Requirement { get; set; } = string.Empty;
    
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("steps")]
    public string Steps { get; set; } = string.Empty;

    [Column("expected")]
    public string Expected { get; set; } = string.Empty;

    [Column("framework")]
    public string Framework { get; set; } = string.Empty;

    [Column("model_used")]
    public string ModelUsed { get; set; } = string.Empty;

    [Column("script_code")]
    public string ScriptCode { get; set; } = string.Empty;

    [Column("latency_ms")]
    public double LatencyMs { get; set; }

    [Column("prompt_tokens")]
    public int PromptTokens { get; set; }

    [Column("completion_tokens")]
    public int CompletionTokens { get; set; }

    [Column("total_tokens")]
    public int TotalTokens { get; set; }

    [Column("total_duration_ms")]
    public double TotalDurationMs { get; set; }

    [Column("prompt_eval_duration_ms")]
    public double PromptEvalDurationMs { get; set; }

    [Column("eval_duration_ms")]
    public double EvalDurationMs { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
