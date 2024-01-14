using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public DateTime DateRegistered { get; set; }
    public UserProfile? UserProfile { get; set; }
    public List<ScienceProject>? ScienceProjects { get; set; }
   
}