using Domain.Dtos.ScientificDirectionCaregoryDtos;

namespace Domain.Dtos.ScientificDirectionDtos;

public class GetScientificDirectionDto : ScientificDirectionDto
{
    public int Id { get; set; }
    public List<GetScientificDirectionCategoryDto>? GetScientificDirectionCategory { get; set; }
}