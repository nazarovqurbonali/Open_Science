namespace Domain.Dtos.ScienceProjectDtos;

public class UpdateScienceProjectDto : AddScienceProjectDto
{
    public int Id { get; set; }
    public int ScientificDirectionCategoryId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}