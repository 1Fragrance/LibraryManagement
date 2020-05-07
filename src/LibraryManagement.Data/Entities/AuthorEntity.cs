using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Common;
using LibraryManagement.Common.Attributes;

namespace LibraryManagement.Data.Entities
{
    /// <summary>
    /// Author model
    /// </summary>
    public class AuthorEntity : EntityBase
    {
        /// <summary>
        /// Author name
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        [SerializationOrder(0)]
        public string Name { get; set; }

        /// <summary>
        /// Author surname
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        [SerializationOrder(1)]
        public string Surname { get; set; }

        /// <summary>
        /// Author patronymic 
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        [SerializationOrder(2)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Relative books
        /// </summary>
        public ICollection<BookEntity> Books { get; set; }
    }
}
