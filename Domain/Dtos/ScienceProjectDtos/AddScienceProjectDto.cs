using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Domain.Dtos.ScienceProjectDtos;

public class AddScienceProjectDto : ScienceProjectDto
{
    public List<int> CategoryId { get; set; }
    public required IFormFile ProjectFileName { get; set; }
    public int ScientificDirectionId { get; set; }
}