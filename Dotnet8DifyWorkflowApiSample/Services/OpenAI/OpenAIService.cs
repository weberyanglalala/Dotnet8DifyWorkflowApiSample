using System.Text.Json;
using Dotnet8DifyWorkflowApiSample.Common;
using Dotnet8DifyWorkflowApiSample.Services.OpenAI.Dtos;
using OpenAI.Chat;

namespace Dotnet8DifyWorkflowApiSample.Services.OpenAI;

public class OpenAIService
{
    private readonly ChatClient _client;

    public OpenAIService(ChatClient client)
    {
        _client = client;
    }

    public async Task<Result<TravelTicketsResponse>> GenerateTravelTickets(string productName)
    {
        List<ChatMessage> messages =
        [
            new UserChatMessage($"請根據商品名稱 {productName} 生成5個旅遊景點"),
        ];

        ChatCompletionOptions options = new()
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "travel_tickets",
                jsonSchema: BinaryData.FromBytes("""
                                                 {
                                                     "type": "object",
                                                     "properties": {
                                                         "tickets": {
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "object",
                                                                 "properties": {
                                                                     "category": {
                                                                         "type": "string",
                                                                         "description": "The category of the attraction."
                                                                     },
                                                                     "location": {
                                                                         "type": "string",
                                                                         "description": "The location of the attraction."
                                                                     },
                                                                     "description": {
                                                                         "type": "string",
                                                                         "description": "A brief description of the attraction."
                                                                     },
                                                                     "coordinates": {
                                                                         "type": "object",
                                                                         "properties": {
                                                                             "latitude": {
                                                                                 "type": "number",
                                                                                 "description": "The latitude coordinate."
                                                                             },
                                                                             "longitude": {
                                                                                 "type": "number",
                                                                                 "description": "The longitude coordinate."
                                                                             }
                                                                         },
                                                                         "required": ["latitude", "longitude"],
                                                                         "additionalProperties": false
                                                                     },
                                                                     "ticket_types": {
                                                                         "type": "array",
                                                                         "description": "Different types of tickets available for the attraction.",
                                                                         "items": {
                                                                             "type": "object",
                                                                             "properties": {
                                                                                 "description": {
                                                                                     "type": "string",
                                                                                     "description": "Description of the ticket type."
                                                                                 },
                                                                                 "reservation_date": {
                                                                                     "type": ["string", "null"],
                                                                                     "description": "The reservation date for the ticket, if applicable."
                                                                                 },
                                                                                 "ticket_type": {
                                                                                     "type": "string",
                                                                                     "description": "The type of the ticket."
                                                                                 },
                                                                                 "attraction_type": {
                                                                                     "type": "string",
                                                                                     "description": "Type of attraction access for this ticket."
                                                                                 },
                                                                                 "options": {
                                                                                     "type": "array",
                                                                                     "description": "Options available for this ticket type.",
                                                                                     "items": {
                                                                                         "type": "object",
                                                                                         "properties": {
                                                                                             "name": {
                                                                                                 "type": "string",
                                                                                                 "description": "Name of the ticket option."
                                                                                             },
                                                                                             "price": {
                                                                                                 "type": "number",
                                                                                                 "description": "Price of the ticket option."
                                                                                             }
                                                                                         },
                                                                                         "required": ["name", "price"],
                                                                                         "additionalProperties": false
                                                                                     }
                                                                                 }
                                                                             },
                                                                             "required": ["description", "reservation_date", "ticket_type", "attraction_type", "options"],
                                                                             "additionalProperties": false
                                                                         }
                                                                     }
                                                                 },
                                                                 "required": ["category", "location", "description", "coordinates", "ticket_types"],
                                                                 "additionalProperties": false
                                                             }
                                                         }
                                                     },
                                                     "required": ["tickets"],
                                                     "additionalProperties": false
                                                 }
                                                 """u8.ToArray()),
                jsonSchemaIsStrict: true)
        };

        ChatCompletion completion = await _client.CompleteChatAsync(messages, options);

        string jsonString = completion.Content[0].Text;

        if (string.IsNullOrEmpty(jsonString))
        {
            return Result<TravelTicketsResponse>.Failure("Empty content received from AI");
        }
        
        try
        {
            var travelTicketsResponse = JsonSerializer.Deserialize<TravelTicketsResponse>(jsonString);
            if (travelTicketsResponse == null)
            {
                return Result<TravelTicketsResponse>.Failure("Deserialization resulted in null");
            }

            return Result<TravelTicketsResponse>.Success(travelTicketsResponse);
        }
        catch (JsonException ex)
        {
            return Result<TravelTicketsResponse>.Failure($"Deserialization failed: {ex.Message}");
        }
    }
}