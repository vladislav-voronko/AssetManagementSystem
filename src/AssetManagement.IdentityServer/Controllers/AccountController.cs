using Duende.IdentityServer.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AssetManagement.IdentityServer.Models;

namespace AssetManagement.IdentityServer.Controllers;

public sealed class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;

    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider)
    {
        _signInManager = signInManager;
        _interaction = interaction;
        _schemeProvider = schemeProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        if (!IsValidReturnUrl(returnUrl))
        {
            return BadRequest();
        }

        return await RenderLoginAsync(new LoginViewModel { ReturnUrl = returnUrl! });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!IsValidReturnUrl(model.ReturnUrl))
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return await RenderLoginAsync(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return Redirect(model.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return await RenderLoginAsync(model);
    }

    [HttpGet]
    public IActionResult Logout(string? logoutId)
    {
        return View(new LogoutViewModel { LogoutId = logoutId ?? string.Empty });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(LogoutViewModel model)
    {
        await _signInManager.SignOutAsync();

        var logoutContext = await _interaction.GetLogoutContextAsync(model.LogoutId);
        if (!string.IsNullOrEmpty(logoutContext?.PostLogoutRedirectUri))
        {
            return Redirect(logoutContext.PostLogoutRedirectUri);
        }

        return RedirectToAction(nameof(LoggedOut));
    }

    [HttpGet]
    public IActionResult LoggedOut()
    {
        return View();
    }

    private bool IsValidReturnUrl(string? returnUrl)
    {
        return !string.IsNullOrWhiteSpace(returnUrl)
            && (Url.IsLocalUrl(returnUrl) || _interaction.IsValidReturnUrl(returnUrl));
    }

    private async Task<IActionResult> RenderLoginAsync(LoginViewModel model)
    {
        model.GoogleLoginEnabled = await _schemeProvider.GetSchemeAsync("Google") is not null;
        return View("Login", model);
    }
}
