using AssetManagement.Api.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Application.Common;
using Identity.Application.Interfaces;
using Identity.Application.Validators;
using Identity.Domain.Entities;
using Identity.Infrastructure.Auth;
using Identity.Infrastructure.Extensions;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using SharedKernel.Db;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "AssetManagementSystem API",
            Version = "v1"
        });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });
});

builder.Services
    .AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<
    RegisterRequestValidator>();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(
        options =>
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

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings!.Issuer,

                ValidAudience = jwtSettings.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings.Secret)),

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddDbInitializers();

ConfigureServices(builder.Configuration, builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await ApplicationInitialization
        .InitializeDatabaseAsync(app.Services);

    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    ResolveDbInitialzers(scope.ServiceProvider);
}

app.Run();

static void ConfigureServices(ConfigurationManager configuration, WebApplicationBuilder builder)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection")!;
    builder.Services.AddServiceLinesDbContexts(connectionString);
}

IEnumerable<IDbInitializer> ResolveDbInitialzers(IServiceProvider serviceProvider)
{
    var initializers = new List<IDbInitializer>();
    var contextTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetExportedTypes())
        .Where(myType => myType.IsClass &&
        !myType.IsAbstract &&
        myType.IsSubclassOf(typeof(DbContext)));
    var dbInitType = typeof(IDbInitializer<>);

    foreach (var contextType in contextTypes.OrderBy(x => x.Name))
    {
        var constructed = dbInitType.MakeGenericType(contextType);
        var initializer = (IDbInitializer)serviceProvider.GetService(constructed)!;

        if (initializer == null)
        {
            continue;
        }

        initializer.MigrateAsync().Wait();
        initializers.Add(initializer);
    }

    return initializers;
}