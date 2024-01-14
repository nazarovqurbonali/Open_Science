using Infrastructure.DataContext;
using Infrastructure.Seed;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.ChatService;
using Infrastructure.Services.EmailService;
using Infrastructure.Services.FileService;
using Infrastructure.Services.LocationService;
using Infrastructure.Services.ScienceProjectService;
using Infrastructure.Services.ScientificDirectionCategoryServices;
using Infrastructure.Services.ScientificDirectionService;
using Infrastructure.Services.UserProfileService;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService;

public static class RegisterService
{
    public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(configure =>
            configure.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<Seeder>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IScienceProjectService, ScienceProjectService>();
        services.AddScoped<IScientificDirectionService, ScientificDirectionService>();
        services.AddScoped<IScientificDirectionCategoryService, ScientificDirectionCategoryService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();
        
        
        services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false; // must have at least one digit
                config.Password.RequireNonAlphanumeric = false; // must have at least one non-alphanumeric character
                config.Password.RequireUppercase = false; // must have at least one uppercase character
                config.Password.RequireLowercase = false;  // must have at least one lowercase character
            })
            //for registering usermanager and signinmanger
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
    }
}