using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Name { get; set; }

        /// <summary>
        /// Author surname
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Surname { get; set; }

        /// <summary>
        /// Author patronymic 
        /// </summary>
        [Required]
        [MaxLength(Constants.DataAnnotationConstants.StringMaxLengthValue)]
        public string Patronymic { get; set; }
    }
}
