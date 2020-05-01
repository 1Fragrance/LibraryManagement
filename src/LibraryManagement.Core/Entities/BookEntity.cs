using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Core.Entities
{
    /// <summary>
    /// Book model
    /// </summary>
    public class BookEntity : EntityBase
    {
        /// <summary>
        /// Book registration number
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string RegNumber { get; set; }

        /// <summary>
        /// Book name
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Name { get; set; }

        /// <summary>
        /// Number of pages in book
        /// </summary>
        [Required]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Year of publication of the book
        /// </summary>
        [Required]
        public int PublicationYear { get; set; }

        /// <summary>
        /// Indicates is book in library
        /// </summary>
        [Required]
        public bool IsBookInLibrary { get; set; }
        
        /// <summary>
        /// Foreign key to the publisher entity
        /// </summary>
        public int? PublisherId { get; set; }
        public PublisherEntity Publisher { get; set; }

        /// <summary>
        /// Foreign key to the author entity
        /// </summary>
        public int? AuthorId { get; set; }
        public AuthorEntity Author { get; set; }

        /// <summary>
        /// Foreign key to the last user who took the book
        /// </summary>
        public int? LastUserId { get; set; }
        public UserEntity LastUser { get; set; }
    }
}
