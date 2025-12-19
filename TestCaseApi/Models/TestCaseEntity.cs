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

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
