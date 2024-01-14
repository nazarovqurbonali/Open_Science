namespace Domain.Filters;

public class GetScienceProjectFilter:PaginationFilter
{
    public string? Name { get; set; }
    public int DirectionId { get; set; }
}