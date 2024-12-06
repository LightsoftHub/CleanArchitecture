using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArch.Shared.Authorization;

public class Permissions
{
    [DisplayName("System permissions")]
    public static class System
    {
        [Display(Name = "Push notifications", Description = "use this to push notifications")]
        public const string Notification = $"{nameof(System)}.{nameof(Notification)}";
    }

    [DisplayName("Tenants")]
    [Description("Tenants Management")]
    public static class Tenants
    {
        private const string root = nameof(Tenants);

        [Display(Name = "View users list")]
        public const string View = $"{root}.{nameof(View)}";
        public const string Create = $"{root}.{nameof(Create)}";
        public const string Update = $"{root}.{nameof(Update)}";
        public const string Delete = $"{root}.{nameof(Delete)}";
    }

    [DisplayName("Users")]
    [Description("Users Management")]
    public static class Users
    {
        private const string root = nameof(Users);

        [Display(Name = "View users list")]
        public const string View = $"{root}.{nameof(View)}";
        public const string Create = $"{root}.{nameof(Create)}";
        public const string Update = $"{root}.{nameof(Update)}";
        public const string Delete = $"{root}.{nameof(Delete)}";
    }

    [Description("Roles Management")]
    public static class Roles
    {
        private const string root = nameof(Roles);

        [Display(Description = "use this to view roles list")]
        public const string View = $"{root}.{nameof(View)}";
        public const string Create = $"{root}.{nameof(Create)}";
        public const string Update = $"{root}.{nameof(Update)}";
        public const string Delete = $"{root}.{nameof(Delete)}";
    }
}