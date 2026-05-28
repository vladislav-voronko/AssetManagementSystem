using Identity.Application.DTOs;
using Identity.Application.DTOs.Requests;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AssetManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IJwtProvider _jwtProvider;

    private readonly IdentityDbContext _context;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IJwtProvider jwtProvider,
        IdentityDbContext context)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _context = context;
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

        await _userManager.AddToRoleAsync(
            user,
            "User");

        var accessToken =
            await _jwtProvider.Generate(user);

        var refreshToken =
            RefreshTokenGenerator.Generate();

        var refreshTokenEntity =
            new RefreshToken
            {
                Token = refreshToken,

                UserId = user.Id,

                ExpiresAt =
                    DateTime.UtcNow.AddDays(7)
            };

        _context.RefreshTokens.Add(
            refreshTokenEntity);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken,
            refreshToken
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginDto dto)
    {
        var user =
            await _userManager.FindByEmailAsync(
                dto.Email);

        if (user is null)
        {
            return Unauthorized();
        }

        var passwordValid =
            await _userManager.CheckPasswordAsync(
                user,
                dto.Password);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        var accessToken =
            await _jwtProvider.Generate(user);

        var refreshToken =
            RefreshTokenGenerator.Generate();

        var refreshTokenEntity =
            new RefreshToken
            {
                Token = refreshToken,

                UserId = user.Id,

                ExpiresAt =
                    DateTime.UtcNow.AddDays(7)
            };

        _context.RefreshTokens.Add(
            refreshTokenEntity);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken,
            refreshToken
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        RefreshTokenRequest request)
    {
        var refreshTokenEntity =
            await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.Token ==
                    request.RefreshToken);

        if (refreshTokenEntity is null)
        {
            return Unauthorized();
        }

        if (refreshTokenEntity.IsRevoked)
        {
            return Unauthorized();
        }

        if (refreshTokenEntity.ExpiresAt <
            DateTime.UtcNow)
        {
            return Unauthorized();
        }

        var accessToken =
            await _jwtProvider.Generate(
                refreshTokenEntity.User);

        return Ok(new
        {
            accessToken
        });
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

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        RefreshTokenRequest request)
    {
        var refreshToken =
            await _context.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.Token ==
                    request.RefreshToken);

        if (refreshToken is null || refreshToken.IsRevoked)
        {
            return Unauthorized();
        }

        refreshToken.IsRevoked = true;

        await _context.SaveChangesAsync();

        return Ok();
    }
}