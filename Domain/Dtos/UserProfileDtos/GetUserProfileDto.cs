using Domain.Dtos.LocationDtos;

namespace Domain.Dtos.UserProfileDtos;

public class GetUserProfileDto : UserProfileDto
{
    public required string UserId { get; set; }
    public GetLocationDto? Location { get; set; }
    public  string? Gender { get; set; }
    
    public string? Avatar { get; set; }
    
    
}