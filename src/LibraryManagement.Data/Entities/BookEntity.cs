using LibraryManagement.Common;
using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Common.Attributes;

namespace LibraryManagement.Data.Entities
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
        [SerializationOrder(0)]
        public string RegNumber { get; set; }

        /// <summary>
        /// Book name
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        [SerializationOrder(1)]
        public string Name { get; set; }

        /// <summary>
        /// Number of pages in book
        /// </summary>
        [Required]
        [SerializationOrder(2)]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Year of publication of the book
        /// </summary>
        [Required]
        [Range(Constants.DataAnnotationConstants.YearMinValue, int.MaxValue)]
        [SerializationOrder(3)]
        public int PublicationYear { get; set; }

        /// <summary>
        /// Indicates is book in library
        /// </summary>
        [Required]
        [SerializationOrder(4)]
        public bool IsBookInLibrary { get; set; }

        /// <summary>
        /// Foreign key to the publisher entity
        /// </summary>
        [SerializationOrder(5)]
        public int? PublisherId { get; set; }
        public PublisherEntity Publisher { get; set; }

        /// <summary>
        /// Foreign key to the author entity
        /// </summary>
        [SerializationOrder(6)]
        public int? AuthorId { get; set; }
        public AuthorEntity Author { get; set; }

        /// <summary>
        /// Foreign key to the last user who took the book
        /// </summary>
        [SerializationOrder(7)]
        public int? LastUserId { get; set; }
        public UserEntity LastUser { get; set; }
    }
}
