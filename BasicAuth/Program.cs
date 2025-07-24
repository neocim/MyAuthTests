using System.Security.Claims;
using BasicAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy(
            "read:messages",
            policy => policy.Requirements.Add(
                new HasScopeRequirement("read:messages",
                    domain
                )
            ));
    });

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();