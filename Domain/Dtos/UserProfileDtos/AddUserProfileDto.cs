using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDtos;

public class AddUserProfileDto: UserProfileDto
{
    public int LocationId { get; set; }
    public Gender Gender { get; set; }
    
    public IFormFile? Avatar { get; set; }
}