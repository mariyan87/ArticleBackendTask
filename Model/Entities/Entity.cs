using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    /// <summary>
    /// Entity is the root of all entities.
    /// Some common properties and methods for all DB entities.
    /// </summary>
    public class Entity
    {
        [Key]
        [Required]
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        //public long ModifiedBy { get; set; }
        //public long CreatedBy { get; set; }
    }
}
