using Dotnet8DifyWorkflowApiSample.Common;
using Dotnet8DifyWorkflowApiSample.Controllers.OpenAI.Dtos;
using Dotnet8DifyWorkflowApiSample.Services.OpenAI;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8DifyWorkflowApiSample.Controllers.OpenAI;

[Route("api/[controller]/[action]")]
[ApiController]
public class OpenAIController : ControllerBase
{
    private readonly OpenAIService _openAIService;

    public OpenAIController(OpenAIService openAiService)
    {
        _openAIService = openAiService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail([FromBody] CreateProductDetailRequest request)
    {
        var response = await _openAIService.GenerateTravelTickets(request.ProductName);

        var apiResponse = new ApiResponse
        {
            IsSuccess = true,
            Code = ApiStatusCode.Success,
            Body = response
        };

        return Ok(apiResponse);
    }
}