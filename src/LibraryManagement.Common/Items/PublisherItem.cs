using System.Collections.Generic;

namespace LibraryManagement.Common.Items
{
    /// <summary>
    /// Publisher model
    /// </summary>
    public class PublisherItem : ItemBase
    {
        /// <summary>
        /// Publisher name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Relative books
        /// </summary>
        public ICollection<BookItem> Books { get; set; }
    }
}
