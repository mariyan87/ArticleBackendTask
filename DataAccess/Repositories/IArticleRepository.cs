using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Search;
using DataAccess.Dto.Article;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Provides methods for database access to Article.
    /// </summary>
    public interface IArticleRepository : IBaseRepository
    {
        /// <summary>
        /// Get all articles asynchonous.
        /// </summary>
        Task<IEnumerable<ArticleDto>> GetAllArticleDtoAsync();

        /// <summary>
        /// Search for articles asynchonous.
        /// </summary>
        Task<IEnumerable<ArticleDto>> SearchAsync(SearchFilterDto filter, List<SearchOrderDto> order, int pageNumber, int itemsPerPage);
    }
}
