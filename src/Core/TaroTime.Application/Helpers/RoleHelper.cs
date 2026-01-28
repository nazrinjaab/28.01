namespace TaroTime.Application.Helpers
{
    using TaroTime.Domain.Enums;

    public static class RoleHelper
    {
        public static string ToIdentityRole(UserRole role)
        {
            return role.ToString(); 
        }
    }
}