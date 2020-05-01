using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Common.Items
{
    /// <summary>
    /// Author model
    /// </summary>
    public class AuthorItem : ItemBase
    {
        /// <summary>
        /// Author name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Author patronymic 
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Relative books
        /// </summary>
        public ICollection<BookItem> Books { get; set; }

        public string DisplayName => $"{Surname} {Name} {Patronymic}";
    }
}
