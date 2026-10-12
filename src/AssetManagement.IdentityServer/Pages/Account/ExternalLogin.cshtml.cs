using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.IdentityServer.Pages.Account;

public sealed class ExternalLoginModel : PageModel
{
    private const string GoogleScheme = "Google";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;

    public ExternalLoginModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
        _schemeProvider = schemeProvider;
    }

    public string ReturnUrl { get; private set; } = string.Empty;

    public string? ErrorMessage { get; private set; }

    public async Task<IActionResult> OnPostChallengeAsync(string provider, string? returnUrl)
    {
        if (provider != GoogleScheme || !IsValidReturnUrl(returnUrl))
        {
            return BadRequest();
        }

        if (await _schemeProvider.GetSchemeAsync(provider) is null)
        {
            return BadRequest();
        }

        var callbackUrl = Url.Page(
            "/Account/ExternalLogin",
            pageHandler: "Callback",
            values: new { returnUrl });

        var properties = new AuthenticationProperties
        {
            RedirectUri = callbackUrl
        };

        return Challenge(properties, provider);
    }

    public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl, string? error)
    {
        if (!IsValidReturnUrl(returnUrl))
        {
            return BadRequest();
        }

        ReturnUrl = returnUrl!;

        if (!string.IsNullOrWhiteSpace(error))
        {
            return await FailAsync("Google sign-in was cancelled or failed.");
        }

        var externalResult = await HttpContext.AuthenticateAsync(
            IdentityServerConstants.ExternalCookieAuthenticationScheme);

        if (!externalResult.Succeeded || externalResult.Principal is null)
        {
            return await FailAsync("Google sign-in could not be completed. Please try again.");
        }

        var principal = externalResult.Principal;
        var providerKey = principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? principal.FindFirstValue("sub");
        var email = principal.FindFirstValue(ClaimTypes.Email)
            ?? principal.FindFirstValue("email");

        if (string.IsNullOrWhiteSpace(providerKey) || string.IsNullOrWhiteSpace(email))
        {
            return await FailAsync("Google did not provide the required account information.");
        }

        var user = await _userManager.FindByLoginAsync(GoogleScheme, providerKey);
        if (user is null)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser is not null)
            {
                return await FailAsync(
                    "An account with this email already exists. Sign in to that account before linking Google.");
            }

            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = principal.FindFirstValue(ClaimTypes.Surname)
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return await FailAsync(string.Join(" ", createResult.Errors.Select(x => x.Description)));
            }

            var loginResult = await _userManager.AddLoginAsync(
                user,
                new UserLoginInfo(GoogleScheme, providerKey, GoogleScheme));

            if (!loginResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return await FailAsync(string.Join(" ", loginResult.Errors.Select(x => x.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return await FailAsync(string.Join(" ", roleResult.Errors.Select(x => x.Description)));
            }
        }

        await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
        await _signInManager.SignInAsync(user, isPersistent: false, authenticationMethod: GoogleScheme);

        return Redirect(ReturnUrl);
    }

    private bool IsValidReturnUrl(string? returnUrl)
    {
        return !string.IsNullOrWhiteSpace(returnUrl)
            && (Url.IsLocalUrl(returnUrl) || _interaction.IsValidReturnUrl(returnUrl));
    }

    private async Task<IActionResult> FailAsync(string message)
    {
        await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
        ErrorMessage = message;
        return Page();
    }
}
