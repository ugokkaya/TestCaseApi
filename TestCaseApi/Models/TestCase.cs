namespace TestCaseApi.Models;

public class TestCase
{
    public string Title { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = new();
    public string Expected { get; set; } = string.Empty;
}