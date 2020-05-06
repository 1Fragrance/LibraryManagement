using LibraryManagement.Common;
using LibraryManagement.Common.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LibraryManagement.Data.Entities
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [SerializationOrder(-1)]
        public int Id { get; set; }

        /// <summary>
        /// Get entity description string for serialization
        /// </summary>
        public string GetEntityDescriptionString()
        {
            var classProperties = this.GetType().GetProperties()
                .Where(r => r.CustomAttributes.Any(w => w.AttributeType == typeof(SerializationOrderAttribute)))
                .OrderBy(w =>  (w.GetCustomAttributes(typeof(SerializationOrderAttribute), false).FirstOrDefault() as SerializationOrderAttribute)?.Order);

            var classPropertiesString = classProperties.Select(w => w.Name).Aggregate((i, j) => i + Constants.Serialization.FileDelimiter + j);

            return $"{GetType().Name}{Constants.Serialization.FileDelimiter}{classPropertiesString}";
        }
    }
}
