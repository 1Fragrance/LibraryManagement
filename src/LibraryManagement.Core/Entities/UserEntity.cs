using LibraryManagement.Core.Enums;

namespace LibraryManagement.Core.Entities
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserEntity : EntityBase
    {
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public RoleType Role { get; set; }
    }
}
