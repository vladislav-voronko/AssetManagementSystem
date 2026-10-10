using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Services;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssetManagement.IdentityServer.Pages.Account;

public sealed class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IIdentityServerInteractionService _interaction;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IIdentityServerInteractionService interaction)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    [BindProperty]
    public string ReturnUrl { get; set; } = string.Empty;

    public IActionResult OnGet(string? returnUrl)
    {
        if (!IsValidReturnUrl(returnUrl))
        {
            return BadRequest();
        }

        ReturnUrl = returnUrl!;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!IsValidReturnUrl(ReturnUrl))
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existingUser = await _userManager.FindByEmailAsync(Input.Email);
        if (existingUser is not null)
        {
            ModelState.AddModelError("Input.Email", "An account with this email already exists.");
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = Input.Email,
            Email = Input.Email
        };

        var createResult = await _userManager.CreateAsync(user, Input.Password);
        if (!createResult.Succeeded)
        {
            AddErrors(createResult.Errors);
            return Page();
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            AddErrors(roleResult.Errors);
            return Page();
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return Redirect(ReturnUrl);
    }

    private bool IsValidReturnUrl(string? returnUrl)
    {
        return !string.IsNullOrWhiteSpace(returnUrl)
            && (Url.IsLocalUrl(returnUrl) || _interaction.IsValidReturnUrl(returnUrl));
    }

    private void AddErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    public sealed class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
