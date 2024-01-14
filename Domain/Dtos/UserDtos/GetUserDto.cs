namespace Domain.Dtos.UserDtos;

public class GetUserDto : UserDto
{
    public required string Avatar { get; set; }
    public required string Id { get; set; }
    public DateTime DateRegistered { get; set; }
}