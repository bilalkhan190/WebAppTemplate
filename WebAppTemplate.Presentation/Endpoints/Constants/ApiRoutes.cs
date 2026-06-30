namespace WebAppTemplate.Presentation.Endpoints.Constants;


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
    
}
