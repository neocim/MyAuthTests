using Microsoft.AspNetCore.Authorization;

namespace BasicAuth;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        HasScopeRequirement requirement)
    {
        Console.WriteLine("IN HANDLER");
        Console.WriteLine("USER:");

        foreach (var claim in context.User.Claims)
            Console.WriteLine($"{claim}");

        if (!context.User.HasClaim(c =>
                c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        var scopes = context.User
            .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)!
            .Value
            .Split(' ');

        Console.WriteLine("SCOPES:");
        foreach (var scope in scopes) Console.WriteLine($"SCOPE: {scope}");

        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}