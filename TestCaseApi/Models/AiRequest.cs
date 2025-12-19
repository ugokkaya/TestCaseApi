namespace TestCaseApi.Models;

public class AiRequest
{
    public string Requirement { get; set; } = string.Empty;
    public string Framework { get; set; } = string.Empty;
    public string? Model { get; set; }
}
