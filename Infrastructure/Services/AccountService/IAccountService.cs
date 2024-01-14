﻿using Domain.Dtos;
using Domain.Dtos.AccountDtos;
using Domain.Responses;

namespace Infrastructure.Services.AccountService;

public interface IAccountService
{
    Task<Response<string>> Register(RegisterDto model);
    Task<Response<string>> Login(LoginDto model);
    Task<Response<string>> ResetPassword(ResetPasswordDto resetPasswordDto);
    Task<Response<string>> ForgotPasswordTokenGenerator(ForgotPasswordDto forgotPasswordDto);
    Task<Response<string>> ChangePassword(ChangePasswordDto passwordDto, string userId);
}