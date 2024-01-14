namespace Domain.Dtos.LocationDtos;

public class LocationDto
{
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? ZipCode { get; set; }
    public string? State { get; set; }
}