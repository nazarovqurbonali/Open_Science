namespace Domain.Entities;

public class Direction
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Category>? Categories { get; set; }
}