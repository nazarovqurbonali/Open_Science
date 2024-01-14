using System.Net;
using Domain.Dtos.LocationDtos;
using Domain.Dtos.ScientificDirectionCaregoryDtos;
using Domain.Dtos.ScientificDirectionDtos;
using Domain.Dtos.UserProfileDtos;
using Domain.Responses;
using Infrastructure.DataContext;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserProfileService;

public class UserProfileService : IUserProfileService
{
    private readonly ApplicationContext _context;
    private readonly IFileService _fileService;

    public UserProfileService(ApplicationContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Response<List<GetUserProfileDto>>> GetUserProfiles()
    {
        try
        {
            var userProfiles = await (from up in _context.UserProfiles
                select new GetUserProfileDto()
                {
                    UserId = up.UserId,
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    Avatar = up.Avatar,
                    Dob = up.Dob,
                    Gender = up.Gender.ToString(),
                    Location = new GetLocationDto()
                    {
                        Id = up.Location.Id,
                        Country = up.Location.Country,
                        City = up.Location.City,
                        ZipCode = up.Location.ZipCode,
                        State = up.Location.ZipCode
                    },
                }).ToListAsync();
            return new Response<List<GetUserProfileDto>>(userProfiles);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserProfileDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetUserProfileDto>> GetUserProfileById(string userId)
    {
        try
        {
            var userProfile = await (from up in _context.UserProfiles
                select new GetUserProfileDto()
                {
                    UserId = up.UserId,
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    Avatar = up.Avatar,
                    Dob = up.Dob,
                    Gender = up.Gender.ToString(),
                    Location = new GetLocationDto()
                    {
                        Id = up.Location.Id,
                        Country = up.Location.Country,
                        City = up.Location.City,
                        ZipCode = up.Location.ZipCode,
                        State = up.Location.ZipCode,
                    },
                }).FirstOrDefaultAsync(w => w.UserId == userId);
            if (userProfile == null)
            {
                return new Response<GetUserProfileDto>(HttpStatusCode.BadRequest, "Not Found");
            }

            return new Response<GetUserProfileDto>(userProfile);
        }
        catch (Exception e)
        {
            return new Response<GetUserProfileDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateUserProfile(AddUserProfileDto userProfile, string userId)
    {
        try
        {
            var request = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (request == null) return new Response<string>(HttpStatusCode.BadRequest, "User not found");
            var del = _fileService.DeleteFile(request.Avatar);
            var avatar = _fileService.CreateFile(userProfile.Avatar);
            request.Avatar = avatar.Data;
            request.FirstName = userProfile.FirstName;
            request.LastName = userProfile.LastName;
            request.Dob = userProfile.Dob;
            request.UserId = userId;
            request.Gender = userProfile.Gender;
            request.LocationId = userProfile.LocationId;

            await _context.SaveChangesAsync();

            return new Response<string>($"{request.UserId}");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserProfile(string userId)
    {
        try
        {
            var userProfile = await _context.UserProfiles.AsNoTracking().FirstOrDefaultAsync(x=>x.UserId==userId);
            if (userProfile == null) return new Response<bool>(HttpStatusCode.BadRequest, "User profile not found!");
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}