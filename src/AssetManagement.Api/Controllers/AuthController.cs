using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AssetManagement.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
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