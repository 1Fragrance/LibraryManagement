namespace LibraryManagement.Core.Entities
{
    /// <summary>
    /// Author model
    /// </summary>
    public class AuthorEntity : EntityBase
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
    }
}
