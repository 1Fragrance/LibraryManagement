using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Common;
using LibraryManagement.Common.Attributes;
using LibraryManagement.Common.Enums;

namespace LibraryManagement.Data.Entities
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
        [SerializationOrder(0)]
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        [SerializationOrder(1)]
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Required]
        [Range((int) RoleType.Client, (int) RoleType.Admin)]
        [SerializationOrder(2)]
        public RoleType Role { get; set; }

        /// <summary>
        /// User library card number
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.LibraryCardNumberMaxLength)]
        [MinLength(Constants.DataAnnotationConstants.LibraryCardNumberMaxLength)]
        [SerializationOrder(3)]
        public string LibraryCardNumber { get; set; } 

        /// <summary>
        /// Books where current user is last who take them
        /// </summary>
        public ICollection<BookEntity> LastTakenBooks { get; set; }
    }
}
