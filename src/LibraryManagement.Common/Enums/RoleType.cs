using System.ComponentModel;

namespace LibraryManagement.Common.Enums
{
    /// <summary>
    /// Account role type enum
    /// </summary>
    public enum RoleType
    {
        [Description("Client type")]
        Client = 0,

        [Description("Admin type")]
        Admin = 1
    }
}
