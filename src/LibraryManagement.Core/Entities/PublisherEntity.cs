using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Entities
{
    /// <summary>
    /// Publisher model
    /// </summary>
    public class PublisherEntity : EntityBase
    {
        /// <summary>
        /// Publisher name
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Name { get; set; }

        /// <summary>
        /// Relative books
        /// </summary>
        public ICollection<BookEntity> Books { get; set; }
    }
}
