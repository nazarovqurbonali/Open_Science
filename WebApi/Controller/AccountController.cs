using System.Net;
using Domain.Dtos;
using Domain.Dtos.AccountDtos;
using Domain.Responses;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

public class AccountController : BaseController
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody]RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.Register(model);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.Login(model);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        if (ModelState.IsValid)
        {
            var result =  await _service.ForgotPasswordTokenGenerator(forgotPasswordDto);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
      
    [HttpDelete("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _service.ResetPassword(resetPasswordDto);
            return StatusCode(result.StatusCode, result);
        }
        
        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPut("ChangePassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")?.Value;
            var result = await _service.ChangePassword(changePasswordDto,userId!);
            return StatusCode(result.StatusCode, result);
        }
        
        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }
}