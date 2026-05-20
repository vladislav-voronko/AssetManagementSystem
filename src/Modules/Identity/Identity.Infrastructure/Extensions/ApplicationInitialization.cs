using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions;

public static class ApplicationInitialization
{
    public static async Task InitializeDatabaseAsync(
        IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<IdentityDbContext>();

        await context.Database.MigrateAsync();

        var roleManager =
            scope.ServiceProvider
                .GetRequiredService<
                    RoleManager<IdentityRole<Guid>>>();

        string[] roles =
        [
            "Admin",
            "Manager",
            "User"
        ];

        foreach (var role in roles)
        {
            var exists =
                await roleManager.RoleExistsAsync(role);

            if (!exists)
            {
                await roleManager.CreateAsync(
                    new IdentityRole<Guid>(role));
            }
        }
    }
}