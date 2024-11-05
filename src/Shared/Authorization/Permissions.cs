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

    [DisplayName("Users")]
    [Description("Users Management")]
    public static class Users
    {
        private const string _user = nameof(Users);

        [Display(Name = "View users list")]
        public const string View = $"{_user}.{nameof(View)}";
        public const string Create = $"{_user}.{nameof(Create)}";
        public const string Update = $"{_user}.{nameof(Update)}";
        public const string Delete = $"{_user}.{nameof(Delete)}";
    }

    [Description("Roles Management")]
    public static class Roles
    {
        private const string _role = nameof(Roles);

        [Display(Description = "use this to view roles list")]
        public const string View = $"{_role}.{nameof(View)}";
        public const string Create = $"{_role}.{nameof(Create)}";
        public const string Update = $"{_role}.{nameof(Update)}";
        public const string Delete = $"{_role}.{nameof(Delete)}";
    }
}