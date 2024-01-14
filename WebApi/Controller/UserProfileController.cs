using Domain.Dtos.UserProfileDtos;
using Infrastructure.Services.UserProfileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class UserProfileController : BaseController
{
    private readonly IUserProfileService _profileService;

    public UserProfileController(IUserProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("get/all/userProfile")]
    public async Task<IActionResult> GetAllUserProfileAsync()
    {
        var result = await _profileService.GetUserProfiles();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("get/userProfile/by/id")]
    public async Task<IActionResult> GetUserProfileByIdAsync(string userId)
    {
        var result = await _profileService.GetUserProfileById(userId);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpPut("update/userProfile")]
    public async Task<IActionResult> UpdateUserProfileAsync([FromForm] AddUserProfileDto profile)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sid").Value;
        var result = await _profileService.UpdateUserProfile(profile, userId);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpDelete("delete/userProfile")]
    public async Task<IActionResult> DeleteUserProfileAsync(string userId)
    {
        var result = await _profileService.DeleteUserProfile(userId);
        return StatusCode(result.StatusCode, result);
    }
}