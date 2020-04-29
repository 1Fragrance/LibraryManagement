using System.Net.NetworkInformation;

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
        public string RegNumber { get; set; }

        /// <summary>
        /// Book name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of pages in book
        /// </summary>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Year of publication of the book
        /// </summary>
        public int PublicationYear { get; set; }

        /// <summary>
        /// Indicates is book in library
        /// </summary>
        public bool IsBookInLibrary { get; set; }
        
        /// <summary>
        /// Foreign key to the publisher entity
        /// </summary>
        public int PublisherId { get; set; }

        /// <summary>
        /// Foreign key to the author entity
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Foreign key to the last user who took the book
        /// </summary>
        public int LastUserId { get; set; }
    }
}
