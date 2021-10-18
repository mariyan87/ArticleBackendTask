using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Entities
{
    /// <summary>
    /// Represents Individual or User.
    /// </summary>
    [Table("Individual")]
    [Index(nameof(Email), IsUnique = true)]
    public class Individual : Entity
    {

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Individual()
        {
        }

        /// <summary>
        /// Email of the Individual. Also used for login.
        /// </summary>
        [Required]
        [MaxLength(360)]
        public string Email { get; set; }

    }
}
