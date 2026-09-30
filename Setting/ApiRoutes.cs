namespace shoppingapi2.Setting;
public class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base= Root + "/" + Version;
    private const string AdminBase =  Base + "/admin";
    private const string ProfileBase =  Base + "/profile";
    public static class Admin
    {
        public const string Product = AdminBase + "/products";
        public const string Order = AdminBase + "/orders";
        public const string User = AdminBase + "/users";

    }
       public static class Profile
    {
        public const string Product = ProfileBase + "/products";
        public const string Order = ProfileBase + "/orders";
        public const string User = ProfileBase + "/users";
        
    }
      public static class Website
    {
        public const string Product = Base + "/products";
        public const string Order = Base + "/orders";
        public const string User = Base + "/users";
        public const string Category= Base + "/categories";
        
    }
    


}