using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserProfileScientificDirection
{
    [Key]
    public int Id { get; set; }
    public required string UserProfileId { get; set; }
    public required UserProfile UserProfile { get; set; }
    public int ScienceDirectionId { get; set; }
    public required ScientificDirection ScientificDirection { get; set; }
}