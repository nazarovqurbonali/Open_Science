using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDtos;

public class UserProfileDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime Dob { get; set; }
    
}