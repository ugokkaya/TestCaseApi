using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestCaseApi.Data;
using TestCaseApi.Models;

namespace TestCaseApi.Services;

public class AiService
{
    private readonly HttpClient _http;
    private readonly OllamaOptions _opts;
    private readonly AppDbContext _db;

    public AiService(HttpClient httpClient, IOptions<OllamaOptions> opts, AppDbContext db)
    {
        _http = httpClient;
        _opts = opts.Value;
        _db = db;
    }

    public async Task<AiResponse> GenerateAsync(AiRequest request, CancellationToken ct = default)
    {
        var payload = new Dictionary<string, object>
        {
            { _opts.Requirement, request.Requirement },
            { _opts.Framework, request.Framework },
            { _opts.Model, string.IsNullOrWhiteSpace(request.Model) ? _opts.DefaultModel : request.Model }
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var url = $"{_opts.GeneratePath.Trim()}";
        var resp = await _http.PostAsync(url, content, ct);
        resp.EnsureSuccessStatusCode();

        var text = await resp.Content.ReadAsStringAsync(ct);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<AiResponse>(text, options);
        if (data is null)
            throw new InvalidOperationException("Cevap alınamadı");

        var modelUsed = string.IsNullOrWhiteSpace(data.ModelUsed)
            ? (string.IsNullOrWhiteSpace(request.Model) ? _opts.DefaultModel : request.Model)
            : data.ModelUsed;

        TestCase? extractedTestCase = null;
        string? extractedScript = null;
        string? extractedScriptCode = null;

        if (data.Result is not null)
        {
            if (data.Result.TestCase is not null)
            {
                extractedTestCase = data.Result.TestCase;
                extractedScript = data.Result.Script;
                extractedScriptCode = data.Result.ScriptCode;
            }
            else if (!string.IsNullOrWhiteSpace(data.Result.RawOutput))
            {
                var raw = data.Result.RawOutput;
                try
                {
                    var parsed = JsonSerializer.Deserialize<AiParsedPayload>(raw, options);
                    if (parsed is not null)
                    {
                        extractedTestCase = parsed.TestCase;
                        extractedScript = parsed.Script;
                        extractedScriptCode = parsed.ScriptCode;
                    }
                }
                catch (JsonException)
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(raw, new JsonDocumentOptions
                        {
                            AllowTrailingCommas = true,
                            CommentHandling = JsonCommentHandling.Skip
                        });

                        var root = doc.RootElement;
                        if (root.ValueKind == JsonValueKind.String)
                        {
                            var inner = root.GetString();
                            if (!string.IsNullOrWhiteSpace(inner))
                            {
                                root = JsonDocument.Parse(inner).RootElement;
                            }
                        }

                        if (root.TryGetProperty("test_case", out var tcEl))
                        {
                            extractedTestCase = tcEl.Deserialize<TestCase>(options);
                        }

                        if (root.TryGetProperty("script", out var scriptEl) && scriptEl.ValueKind == JsonValueKind.String)
                        {
                            extractedScript = scriptEl.GetString();
                        }

                        if (root.TryGetProperty("script_code", out var scriptCodeEl) && scriptCodeEl.ValueKind == JsonValueKind.String)
                        {
                            extractedScriptCode = scriptCodeEl.GetString();
                        }
                    }
                    catch
                    {
                        // ignore;
                    }
                }
            }
        }

        var testCase = extractedTestCase ?? data.TestCase ?? new TestCase();
        var scriptCode = !string.IsNullOrWhiteSpace(extractedScriptCode) ? extractedScriptCode : data.ScriptCode;
        var script = !string.IsNullOrWhiteSpace(extractedScript) ? extractedScript : data.Script;

        data.TestCase = testCase;
        data.Script = script;
        data.ScriptCode = scriptCode ?? string.Empty;
        
        data.Result = null;
        
        var entity = new TestCaseEntity
        {
            Requirement = request.Requirement,
            Title = testCase.Title,
            Steps = System.Text.Json.JsonSerializer.Serialize(testCase.Steps),
            Expected = testCase.Expected,
            Framework = request.Framework,
            ModelUsed = modelUsed,
            ScriptCode = System.Text.Json.JsonSerializer.Serialize(scriptCode),
            LatencyMs = data.Metrics?.LatencyMs ?? 0,
            PromptTokens = data.Metrics?.PromptTokens ?? 0,
            CompletionTokens = data.Metrics?.CompletionTokens ?? 0,
            TotalTokens = data.Metrics?.TotalTokens ?? 0,
            TotalDurationMs = data.Metrics?.TotalDurationMs ?? 0,
            PromptEvalDurationMs = data.Metrics?.PromptEvalDurationMs ?? 0,
            EvalDurationMs = data.Metrics?.EvalDurationMs ?? 0,
            CreatedAt = DateTime.Now
        };

        _db.TestCases.Add(entity);
        await _db.SaveChangesAsync(ct);

        return data;
    }
    
    public async Task<List<TestCaseEntity>> ListAsync(CancellationToken ct = default)
    {
        return await _db.TestCases.ToListAsync(ct);
    }

}
