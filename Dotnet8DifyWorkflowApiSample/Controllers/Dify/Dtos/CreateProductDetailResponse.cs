using System.Text.Json.Serialization;

namespace Dotnet8DifyWorkflowApiSample.Controllers.Dify.Dtos;

public class CreateProductDetailResponse
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("category_name")]
    public string CategoryName { get; set; }
    
    [JsonPropertyName("prompt_id")]
    public string PromptId { get; set; }
}