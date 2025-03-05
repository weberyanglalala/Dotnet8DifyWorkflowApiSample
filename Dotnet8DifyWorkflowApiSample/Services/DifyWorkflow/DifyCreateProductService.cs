using System.Net.Http.Headers;
using Dotnet8DifyWorkflowApiSample.Services.DifyWorkflow.Dtos;

namespace Dotnet8DifyWorkflowApiSample.Services.DifyWorkflow;

public class DifyCreateProductService
{
    private readonly string _difyApiUrl;
    private readonly string _difyCreateProductDetailApiKey;
    private readonly IHttpClientFactory _httpClientFactory;

    public DifyCreateProductService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _difyApiUrl = configuration["DifyWorkFlowApiEndpoint"];
        _difyCreateProductDetailApiKey = configuration["DifyCreateProductDetailApiKey"];
        _httpClientFactory = httpClientFactory;
    }

    public async Task<DifyWorkflowResponse> CreateProductDetail(DifyWorkflowRequest request)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _difyCreateProductDetailApiKey);
    
        var endpoint = $"{_difyApiUrl}/workflows/run";
        var response = await client.PostAsJsonAsync(endpoint, request);
    
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error running workflow: {errorResponse}");
        }
    
        return await response.Content.ReadFromJsonAsync<DifyWorkflowResponse>() ?? throw new Exception("Error reading response");
    }
}