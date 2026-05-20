using Identity.Application.DTOs;
using Identity.Application.DTOs.Requests;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(
        UserManager<ApplicationUser> userManager,
        IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
                return Unauthorized();

            var passwordValid =
                await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
                return Unauthorized();

            var token = _jwtProvider.Generate(user);

            return Ok(new
            {
                accessToken = token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser =
                await _userManager.FindByEmailAsync(request.Email);

            if (existingUser is not null)
            {
                return BadRequest("User already exists");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = _jwtProvider.Generate(user);

            return Ok(new
            {
                accessToken = token
            });
        }
    }
}
