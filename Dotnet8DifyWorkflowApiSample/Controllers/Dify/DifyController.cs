using Dotnet8DifyWorkflowApiSample.Common;
using Dotnet8DifyWorkflowApiSample.Controllers.Dify.Dtos;
using Dotnet8DifyWorkflowApiSample.Services.DifyWorkflow;
using Dotnet8DifyWorkflowApiSample.Services.DifyWorkflow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet8DifyWorkflowApiSample.Controllers.Dify;

[Route("api/[controller]/[action]")]
[ApiController]
public class DifyController : ControllerBase
{
    private readonly DifyCreateProductService _difyCreateProductService;
    private readonly string _difyUserId;

    public DifyController(DifyCreateProductService difyCreateProductService, IConfiguration configuration)
    {
        _difyCreateProductService = difyCreateProductService;
        _difyUserId = configuration["DifyUserId"];
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductDetail([FromBody] CreateWorkflowRequest request)
    {
        var inputs = new Dictionary<string, object>();
        inputs.Add("product_name", request.ProductName);
        
        var runWorkflowRequest = new DifyWorkflowRequest
        {
            Inputs = inputs,
            ResponseMode = "blocking",
            User = _difyUserId
        };
        
        var response = await _difyCreateProductService.CreateProductDetail(runWorkflowRequest);
        var apiResponse = new ApiResponse
        {
            IsSuccess = true,
            Code = ApiStatusCode.Success,
            Body = response.Data.Outputs
        };
        
        return Ok(apiResponse);
    }
}