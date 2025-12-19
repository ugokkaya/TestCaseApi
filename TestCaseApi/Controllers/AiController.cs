using Microsoft.AspNetCore.Mvc;
using TestCaseApi.Models;
using TestCaseApi.Services;

namespace TestCaseApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly AiService _ai;

    public AiController(AiService ai)
    {
        _ai = ai;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromBody] AiRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Requirement))
            return BadRequest("prompt boş olamaz.");
        
        if (string.IsNullOrWhiteSpace(req.Framework))
            return BadRequest("framework boş olamaz.");

        var result = await _ai.GenerateAsync(req, ct);
        return Ok(result);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await _ai.ListAsync(ct);
        return Ok(result);
    }
}