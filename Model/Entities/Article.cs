using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities
{
    [Table("Article")]
    public class Article : Entity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Body { get; set; }
    }
}
