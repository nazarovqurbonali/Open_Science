namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public required string CategoryName { get; set; }
    public int DirectionId { get; set; }
    public Direction? Direction { get; set; }
}