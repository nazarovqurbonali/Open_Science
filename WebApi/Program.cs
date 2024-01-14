using Domain.Dtos.AccountDtos;
using Infrastructure.AutoMapper;
using Infrastructure.DataContext;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using WebApi.ExtensionMethods.AuthConfiguration;
using WebApi.ExtensionMethods.RegisterService;
using WebApi.ExtensionMethods.SwaggerConfiguration;

var builder = WebApplication.CreateBuilder(args);

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig!);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// connection to database && dependency injection
builder.Services.AddRegisterService(builder.Configuration);
builder.Services.AddCors();

// register swagger configuration
builder.Services.SwaggerService();

// authentications service
builder.Services.AddAuthConfigureService(builder.Configuration);

// automapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

app.UseCors(
    build => build.WithOrigins("http://127.0.0.1:5500", "http://localhost:3000","https://localhost:3000","https://info-sphere.netlify.app/","https://info-sphere.netlify.app:3000", "https://info-sphere.netlify.app")
        .AllowAnyHeader()
        .AllowAnyMethod()
);

try
{
    var serviceProvider = app.Services.CreateScope().ServiceProvider; 
    var dataContext = serviceProvider.GetRequiredService<ApplicationContext>();
    await dataContext.Database.MigrateAsync();
    
    // seed data
     var seeder = serviceProvider.GetRequiredService<Seeder>();
     await seeder.SeedRole();
     await seeder.SeedLocation();
     await seeder.SeedUser();
}
catch (Exception)
{
    // ignored
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();