﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class ChangePasswordDto
{
    public required string OldPassword { get; set; }
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Compare("Password"), DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }

}