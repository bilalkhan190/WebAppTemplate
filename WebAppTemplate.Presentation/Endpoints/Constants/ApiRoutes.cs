namespace WebAppTemplate.Presentation.Endpoints.Constants;

/// <summary>
/// Central route prefixes — change yahan, poori API update ho jati hai.
/// Future: Platform.* aur Tenant.* groups add karo jab Application split ho.
/// </summary>
public static class ApiRoutes
{
    public const string ApiRoot = "/api";

    public static class Account
    {
        public const string Base = $"{ApiRoot}/Account";
    }

    public static class User
    {
        public const string Base = $"{ApiRoot}/User";
    }

    public static class Platform
    {
        public const string Base = $"{ApiRoot}/Platform";
    }
    //
    // public static class Tenant
    // {
    //     public const string Base = $"{ApiRoot}/tenant";
    //     public const string Auth = $"{Base}/auth";
    //     public const string Users = $"{Base}/users";
    // }
}
