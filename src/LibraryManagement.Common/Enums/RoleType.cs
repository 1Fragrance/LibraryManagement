using System.ComponentModel;

namespace LibraryManagement.Common.Enums
{
    /// <summary>
    /// Account role type enum
    /// </summary>
    public enum RoleType
    {
        [Description("Клиент")]
        Client = 0,

        [Description("Админ")]
        Admin = 1
    }
}
