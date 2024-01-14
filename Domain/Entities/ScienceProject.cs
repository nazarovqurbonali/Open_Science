using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ScienceProject
{
    public string UserProfileId { get; set; } = null!;
    public UserProfile? UserProfile { get; set; }
    [Key]
    public int Id { get; set; }

    public int  ScientificDirectionId { get; set; }

    public ScientificDirection? ScientificDirection { get; set; }
    public required string Name { get; set; }
    public required string ProjectFileName { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    
}