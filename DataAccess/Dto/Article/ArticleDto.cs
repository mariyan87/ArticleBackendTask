using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dto.Article
{
    public class ArticleDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public Model.Entities.Article ToEntity()
        {
            return new Model.Entities.Article
            {
                Title = Title,
                Body = Body
            };
        }
    }
}
