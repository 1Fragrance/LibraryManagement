using System.ComponentModel.DataAnnotations;
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
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Required]
        [Range((int) RoleType.Client, (int) RoleType.Admin)]
        public RoleType Role { get; set; }
    }
}
