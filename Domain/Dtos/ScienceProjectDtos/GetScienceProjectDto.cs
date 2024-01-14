namespace Domain.Dtos.ScienceProjectDtos;

public class GetScienceProjectDto : ScienceProjectDto
{
    public string UserId { get; set; } = null!;
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string ProjectFileName { get; set; }
    public required string  ScientificDirectionName { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public List<string>? Category { get; set; }
}