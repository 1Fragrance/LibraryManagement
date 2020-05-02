namespace LibraryManagement.Common.Filters
{
    public class BookFilter
    {
        public string Name { get; set; }
        public string RegNumber { get; set; }
        public int? NumberOfPages { get; set; }
        public int? PublicationYear { get; set; }
        public bool? IsBookInLibrary { get; set; }
        public string PublisherName { get; set; }
        public string AuthorName { get; set; }
        public string LastUserName { get; set; }
    }
}
