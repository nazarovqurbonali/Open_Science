using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class ScientificDirectionCategory
{
    public int Id { get; set; }
    public required string CategoryName { get; set; }
    public int ScientificDirectionId { get; set; }
    public ScientificDirection? ScientificDirection { get; set; }
}