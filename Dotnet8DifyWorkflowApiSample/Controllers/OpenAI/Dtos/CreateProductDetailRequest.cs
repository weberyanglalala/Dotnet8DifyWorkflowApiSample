using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotnet8DifyWorkflowApiSample.Controllers.OpenAI.Dtos;

public class CreateProductDetailRequest
{
    [JsonPropertyName("product_name")]
    [Required]
    [Length(5, 30)]
    public string ProductName { get; set; }
}