using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class ScientificDirection
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public List<ScientificDirectionCategory>? ScientificDirectionCategories { get; set; }
    public List<ScienceProject>? ScienceProjects { get; set; }
}