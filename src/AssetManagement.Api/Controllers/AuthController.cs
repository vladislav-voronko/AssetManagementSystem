using Identity.Application.DTOs.Requests;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AssetManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request)
    {
        var existingUser =
            await _userManager.FindByEmailAsync(
                request.Email);

        if (existingUser is not null)
        {
            return BadRequest(
                "User already exists");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result =
            await _userManager.CreateAsync(
                user,
                request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var roleResult = await _userManager.AddToRoleAsync(
            user,
            "User");

        if (!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
        }

        return Ok(new { message = "Account created. Sign in to continue." });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var id = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                 ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email)
                    ?? User.FindFirstValue(ClaimTypes.Email);

        var roles =
            User.FindAll(ClaimTypes.Role)
                .Select(x => x.Value);

        return Ok(new
        {
            id,
            email,
            roles
        });
    }
}