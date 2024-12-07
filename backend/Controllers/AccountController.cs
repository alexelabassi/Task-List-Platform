using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_List_Platform.Dtos.Account;
using Task_List_Platform.Models;
using Task_List_Platform.Service;

namespace Task_List_Platform.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TokenService _tokenService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, TokenService tokenService, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var applicationUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            
            var createdUser = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(new NewUserDto
                    {
                        Username = applicationUser.UserName,
                        Email = applicationUser.Email,
                        Token = _tokenService.CreateToken(applicationUser)
                    });
                }
                else
                {
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                return BadRequest(createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        
        if (user==null) 
            return Unauthorized("Invalid username!");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        if(!result.Succeeded)
            return Unauthorized("Invalid username or password!");

        return Ok(
            new NewUserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
    }
}