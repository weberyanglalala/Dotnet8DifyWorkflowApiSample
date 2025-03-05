using System.Text.Json.Serialization;

namespace Dotnet8DifyWorkflowApiSample.Services.OpenAI.Dtos;

public class TravelTicketsResponse
{
    [JsonPropertyName("tickets")]
    public List<TravelTicket> Tickets { get; set; }
}

public class TravelTicketOption
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }
}

public class TravelTicketType
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("reservation_date")]
    public string? ReservationDate { get; set; }

    [JsonPropertyName("ticket_type")]
    public string TicketType { get; set; }

    [JsonPropertyName("attraction_type")]
    public string AttractionType { get; set; }

    [JsonPropertyName("options")]
    public List<TravelTicketOption> Options { get; set; }
}

public class TravelTicketCoordinates
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

public class TravelTicket
{
    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("coordinates")]
    public TravelTicketCoordinates Coordinates { get; set; }

    [JsonPropertyName("ticket_types")]
    public List<TravelTicketType> TicketTypes { get; set; }
}