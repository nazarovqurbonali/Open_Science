using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class UserProfile
{
    [Key]
    public required string UserId { get; set; }
    public User? User { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime Dob { get; set; }
    public Gender Gender { get; set; }
    public string? Avatar { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public List<UserProfileScientificDirection>? UserProfileScientificDirections { get; set; }
}