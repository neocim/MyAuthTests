using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterAuth.Controllers;

[Route("account")]
public class AccountController : Controller
{
    [HttpPost("login")]
    public async Task Login()
    {
        var authProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme,
            authProperties);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        Console.WriteLine($"Access token:\n{accessToken}");

        return View(new UserProfileViewModel(User.Identity!.Name!, User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)
            ?.Value!));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task Logout()
    {
        var authProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri("/").Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme,
            authProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}