using Domain.Dtos.UserProfileDtos;
using Domain.Responses;

namespace Infrastructure.Services.UserProfileService;

public interface IUserProfileService
{
    Task<Response<List<GetUserProfileDto>>> GetUserProfiles();
    Task<Response<GetUserProfileDto>> GetUserProfileById(string userId);
    Task<Response<string>> UpdateUserProfile(AddUserProfileDto userProfile,string userId);
    Task<Response<bool>> DeleteUserProfile(string id);
}