using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Search;
using DataAccess.Dto.Article;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DataAccessImpl.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IArticleRepository"/>.
    /// </summary>
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        private readonly IDbContext dbContext;

        /// <inheritdoc />
        public ArticleRepository(IDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArticleDto>> GetAllArticleDtoAsync()
        {
            var result = from article in GetEntities<Article>()
                         select new ArticleDto
                         {
                             Title = article.Title,
                             Body = article.Body
                         };

            return await result.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArticleDto>> SearchAsync(SearchFilterDto filter, List<SearchOrderDto> order, int pageNumber, int itemsPerPage)
        {
            var searchQuery = from article in GetEntities<Article>()
                              select article;

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Title))
                {
                    searchQuery = searchQuery.Where(a => a.Title.Contains(filter.Title.Trim()));
                }

                if (!string.IsNullOrWhiteSpace(filter.Body))
                {
                    searchQuery = searchQuery.Where(a => a.Body.Contains(filter.Body.Trim()));
                }
            }

            var orderedArticles = base.OrderAndPaginate<Article>(searchQuery, order, pageNumber, itemsPerPage);

            var filteredAndOrderedArticleDtos = await orderedArticles.Select(a => new ArticleDto
            {
                Title = a.Title,
                Body = a.Body
            })
            .ToListAsync();

            return filteredAndOrderedArticleDtos;
        }
    }
}
