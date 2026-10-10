using System.ComponentModel.DataAnnotations;

namespace AssetManagement.IdentityServer.Models;

public sealed class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }

    [Required]
    public string ReturnUrl { get; set; } = string.Empty;
}
