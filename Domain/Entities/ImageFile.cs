namespace Domain.Entities;

public class ImageFile
{
    public int Id { get; set; }
    public required string FileName { get; set; }
    public DateTime DateCreated { get; set; }
}