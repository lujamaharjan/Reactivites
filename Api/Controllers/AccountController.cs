using System.Security.Claims;
using Api.DTOs;
using Api.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    TokenService tokenService) : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized("Invalid email");

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized();

        return Ok(CreateUserDto(user));
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await userManager.FindByEmailAsync(registerDto.Email) != null)
        {
            return BadRequest("Email taken");
        }

        if (await userManager.FindByNameAsync(registerDto.Username) != null)
        {
            return BadRequest("Username taken");
        }

        var user = new AppUser(registerDto.DisplayName, registerDto.Email, registerDto.Username);
        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return CreateUserDto(user);
        }

        return BadRequest(result.Errors);
    }
    
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
        return Ok(CreateUserDto(user));
    }
    
    private UserDto CreateUserDto(AppUser user)
    {
        return new UserDto()
        {
            DisplayName = user.DisplayName,
            Image = "",
            Token = tokenService.CreateToken(user),
            Username = user.UserName
        };
    }
}