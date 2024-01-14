using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("UserId", "FollowingId", IsUnique = true)]
public class FollowingRelationShip
{
    [Key]
    public int Id { get; set; }
    public required string UserId { get; set; }
    public required User User { get; set; }
    public required string FollowingId { get; set; }
    public required User Following { get; set; }
    public DateTime DateFollowed { get; set; }
}