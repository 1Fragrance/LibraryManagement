using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Core.Entities
{
    /// <summary>
    /// Base model
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Entity id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
