using Dotnet8DifyWorkflowApiSample.Common;
using Dotnet8DifyWorkflowApiSample.Controllers.OpenAI.Dtos;
using Dotnet8DifyWorkflowApiSample.Services.OpenAI;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8DifyWorkflowApiSample.Controllers.OpenAI;

[Route("api/[controller]/[action]")]
[ApiController]
public class OpenAiController : ControllerBase
{
    private readonly OpenAIService _openAiService;

    public OpenAiController(OpenAIService openAiService)
    {
        _openAiService = openAiService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail([FromBody] CreateProductDetailRequest request)
    {
        var result = await _openAiService.GenerateTravelTickets(request.ProductName);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        var response = new ApiResponse
        {
            IsSuccess = true,
            Code = ApiStatusCode.Success,
            Body = result.Value.Tickets
        };

        return Ok(response);
    }
}