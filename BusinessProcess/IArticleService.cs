using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Search;
using DataAccess.Dto.Article;

namespace BusinessProcess
{
    /// <summary>
    /// Provides methods for article operations.
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Get all Article entities from Database
        /// </summary>
        Task<IEnumerable<ArticleDto>> GetAllArticleDtoAsync();

        /// <summary>
        /// Insert Article in Database asynchronous
        /// </summary>
        Task CreateArticleAsync(ArticleDto articleDto);

        /// <summary>
        /// Delete Article with given Id asynchronous.
        /// </summary>
        Task DeleteArticleAsync(long Id);

        /// <summary>
        /// Update Article with given Id asynchronous.
        /// </summary>
        Task UpdateArticleAsync(long id, ArticleDto articleDto);

        /// <summary>
        /// Search Articles by given filter asynchronous.
        /// </summary>
        Task<IEnumerable<ArticleDto>> SearchAsync(SearchFilterDto filter, List<SearchOrderDto> order, int pageNumber, int itemsPerPage);
    }
}
