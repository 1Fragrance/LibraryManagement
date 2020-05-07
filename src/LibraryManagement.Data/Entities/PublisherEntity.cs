using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Common;
using LibraryManagement.Common.Attributes;

namespace LibraryManagement.Data.Entities
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
        [SerializationOrder(0)]
        public string Name { get; set; }

        /// <summary>
        /// Relative books
        /// </summary>
        public ICollection<BookEntity> Books { get; set; }
    }
}
