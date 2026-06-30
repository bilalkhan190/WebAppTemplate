using Microsoft.AspNetCore.Authorization;

namespace WebAppTemplate.Presentation.Authorization;

public sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
