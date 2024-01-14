namespace Domain.Entities;

public class Location
{
    public int Id { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? ZipCode { get; set; }
    public string? State { get; set; }
    public List<UserProfile>? UserProfiles { get; set; }
}