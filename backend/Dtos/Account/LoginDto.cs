﻿using System.ComponentModel.DataAnnotations;

namespace Task_List_Platform.Dtos.Account;

public class LoginDto
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
}