namespace TestCaseApi.Models;

public class OllamaOptions
{
    public string BaseUrl { get; set; }
    public string GeneratePath { get; set; }
    public string Requirement { get; set; }
    public string Framework { get; set; }
    public string Model { get; set; } = "model";
    public string DefaultModel { get; set; } = "qwen";
}
