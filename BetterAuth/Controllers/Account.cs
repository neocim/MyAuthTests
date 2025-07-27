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
    public async Task Login(string redirectPath = "/")
    {
        var authProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirectPath).Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme,
            authProperties);
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        ViewData["Message"] = "Your application description page.";

        return View((User.Identity!.Name, User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)
            ?.Value));
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