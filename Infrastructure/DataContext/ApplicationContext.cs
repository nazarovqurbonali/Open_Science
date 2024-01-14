using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        // Database.EnsureDeleted();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserProfileScientificDirection>()
            .HasKey(us => new { us.UserProfileId, ScienceProjectId = us.ScienceDirectionId });
        base.OnModelCreating(builder);
        
            builder.Entity<Direction>()
            .HasIndex(u => u.Name)
            .IsUnique();
            
            builder.Entity<Category>()
                .HasIndex(u => u.CategoryName)
                .IsUnique();
    }

    public new required DbSet<User> Users { get; set; }
    public required DbSet<UserProfile> UserProfiles { get; set; }
    public required DbSet<ImageFile> ImageFiles { get; set; }
    public required DbSet<Location> Locations { get; set; }
    public required DbSet<ScienceProject> ScienceProjects { get; set; }
    public required DbSet<ScientificDirection> ScientificDirections { get; set; }
    public required DbSet<ScientificDirectionCategory> ScientificDirectionCategories { get; set; }
    public required DbSet<UserProfileScientificDirection> UserProfileScienceDirections { get; set; }

    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Direction> Directions { get; set; }
}