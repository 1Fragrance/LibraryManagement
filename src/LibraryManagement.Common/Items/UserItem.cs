using System.Collections.Generic;
using LibraryManagement.Common.Enums;

namespace LibraryManagement.Common.Items
{
    public class UserItem : ItemBase
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

        /// <summary>
        /// User library card number
        /// </summary>
        public string LibraryCardNumber { get; set; }

        /// <summary>
        /// Books where current user is last who take them
        /// </summary>
        public ICollection<BookItem> LastTakenBooks { get; set; }
    }
}
