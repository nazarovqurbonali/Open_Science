using Domain.Entities;
using Domain.Enums;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder
{
    private readonly ApplicationContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public Seeder(ApplicationContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedRole()
    {
        var newroles = new List<IdentityRole>()
        {
            new(Roles.Admin),
            new(Roles.User)
        };

        var existing = _roleManager.Roles.ToList();
        foreach (var role in newroles)
        {
            if (existing.Exists(e => e.Name == role.Name) == false)
            {
                await _roleManager.CreateAsync(role);
            }
        }
    }
    
    public async Task SeedLocation()
    {
        var locations = await _context.Locations.FindAsync(1);
        if (locations != null) return;
        var location = new Location()
        {
            Id = 1,
            City = "",
            Country = "",
            State = "",
            ZipCode = ""
        };
        await _context.Locations.AddAsync(location);
        await _context.SaveChangesAsync();
    }

    public async Task SeedUser()
    {
        var existing = await _userManager.FindByNameAsync("admin");
        if (existing != null) return;
        var identity = new User()
        {
            UserName = "admin",
            PhoneNumber = "+992005442641",
            Email = "shinoyatzoda@gmail.com",
            DateRegistered = DateTime.UtcNow,
            
        };
        await _userManager.CreateAsync(identity, "hello123");
        await _userManager.AddToRoleAsync(identity, Roles.Admin);

        var profileAdmin = new UserProfile()
        {
            UserId = identity.Id,
            FirstName = string.Empty,
            LastName = string.Empty,
            LocationId = 1,
            Dob = DateTime.UtcNow,
            // Image = string.Empty,
            // About = string.Empty,
            Gender = Gender.Female,
            Avatar = string.Empty
        };
        await _context.UserProfiles.AddAsync(profileAdmin);
        await _context.SaveChangesAsync();
    }
}

public class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}