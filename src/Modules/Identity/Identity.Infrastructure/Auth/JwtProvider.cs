using Identity.Application.Common;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    private readonly UserManager<ApplicationUser> _userManager;

    public JwtProvider(
        IOptions<JwtSettings> jwtOptions,
        UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtOptions.Value;

        _userManager = userManager;
    }

    public async Task<string> Generate(
        ApplicationUser user)
    {
        var roles =
            await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()),

            new(
                JwtRegisteredClaimNames.Email,
                user.Email!)
        };

        claims.AddRange(
            roles.Select(role =>
                new Claim(
                    ClaimTypes.Role,
                    role)));

        var signingCredentials =
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpirationInMinutes),
            signingCredentials: signingCredentials);

        var tokenValue =
            new JwtSecurityTokenHandler()
                .WriteToken(token);

        return tokenValue;
    }
}