using Microsoft.AspNetCore.Authorization;

namespace BasicAuth;

public class HasScopeRequirement : IAuthorizationRequirement
{
    public HasScopeRequirement(string scope, string issuer)
    {
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }

    public string Scope { get; }
    public string Issuer { get; }
}