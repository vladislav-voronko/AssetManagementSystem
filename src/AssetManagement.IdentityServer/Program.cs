using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Identity.Domain.Entities;
using Identity.Infrastructure.Extensions;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

var spaOrigin = builder.Configuration["Oidc:SpaOrigin"]
    ?? throw new InvalidOperationException("OIDC SPA origin was not configured.");
var clientId = builder.Configuration["Oidc:ClientId"]
    ?? throw new InvalidOperationException("OIDC client ID was not configured.");
var apiScope = builder.Configuration["Oidc:ApiScope"]
    ?? throw new InvalidOperationException("OIDC API scope was not configured.");

builder.Services.AddIdentityDbContext(connectionString);

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();

var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

if (string.IsNullOrWhiteSpace(googleClientId) != string.IsNullOrWhiteSpace(googleClientSecret))
{
    throw new InvalidOperationException(
        "Both Google ClientId and ClientSecret must be configured together.");
}

if (!string.IsNullOrWhiteSpace(googleClientId))
{
    builder.Services.AddAuthentication()
        .AddGoogle("Google", options =>
        {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret!;
            options.CallbackPath = "/signin-google";
            options.Events.OnRemoteFailure = context =>
            {
                var callbackUrl = context.Properties?.RedirectUri;
                context.HandleResponse();
                context.Response.Redirect(string.IsNullOrWhiteSpace(callbackUrl)
                    ? "/"
                    : QueryHelpers.AddQueryString(callbackUrl, "error", "external_login_failed"));
                return Task.CompletedTask;
            };
        });
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var identityServerBuilder = builder.Services
    .AddIdentityServer()
    .AddAspNetIdentity<ApplicationUser>()
    .AddInMemoryIdentityResources(new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email()
    })
    .AddInMemoryApiScopes(new ApiScope[]
    {
        new ApiScope(apiScope)
    })
    .AddInMemoryApiResources(new ApiResource[]
    {
        new ApiResource(apiScope)
        {
            Scopes = { apiScope }
        }
    })
    .AddInMemoryClients(new Client[]
    {
        new Client
        {
            ClientId = clientId,
            ClientName = "Asset Management Web",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            AllowOfflineAccess = true,
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                apiScope,
                IdentityServerConstants.StandardScopes.OfflineAccess
            },
            RedirectUris = { $"{spaOrigin}/auth/callback" },
            PostLogoutRedirectUris = { $"{spaOrigin}/" },
            AllowedCorsOrigins = { spaOrigin },
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Absolute,
            AbsoluteRefreshTokenLifetime = 86400
        }
    });

if (builder.Environment.IsDevelopment())
{
    identityServerBuilder.AddDeveloperSigningCredential();
}
else
{
    throw new InvalidOperationException(
        "Configure a production signing credential before running IdentityServer outside Development.");
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await ApplicationInitialization.InitializeDatabaseAsync(app.Services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
