using AssetManagement.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Shared.Infrastructure.Db;
using System.Data;

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

var authority = builder.Configuration["Authentication:Authority"]
    ?? throw new InvalidOperationException("IdentityServer authority was not configured.");
var audience = builder.Configuration["Authentication:Audience"]
    ?? throw new InvalidOperationException("API audience was not configured.");
var apiScope = builder.Configuration["Authentication:Scope"]
    ?? throw new InvalidOperationException("API scope was not configured.");
var spaOrigin = builder.Configuration["Authentication:SpaOrigin"]
    ?? throw new InvalidOperationException("SPA origin was not configured.");

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
        options.Authority = authority;
        options.Audience = audience;
        options.RequireHttpsMetadata = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireClaim("scope", apiScope)
        .Build();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Spa", policy =>
        policy.WithOrigins(spaOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddModules(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Spa");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    ResolveDbInitialzers(scope.ServiceProvider);
}

app.Run();

IEnumerable<IDbInitializer> ResolveDbInitialzers(IServiceProvider serviceProvider)
{
    var initializers = new List<IDbInitializer>();
    var contextTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetExportedTypes())
        .Where(myType => myType.IsClass 
        && !myType.IsAbstract 
        && myType.IsSubclassOf(typeof(DbContext)));
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