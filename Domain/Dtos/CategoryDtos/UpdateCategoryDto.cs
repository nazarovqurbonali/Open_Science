using Domain.Dtos.DirectionDtos;

namespace Domain.Dtos.CategoryDtos;

public class UpdateCategoryDto : AddCategoryDto
{
    public int Id { get; set; }
}