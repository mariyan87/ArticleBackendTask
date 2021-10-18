using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessProcess;
using Common;
using Common.Search;
using DataAccess.Dto.Article;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using WebApi.Dto;
using WebApi.Dto.Search;
using WebApi.Extensions;

namespace WebApi.Controllers.Metrics
{
    /// <summary>
    /// Controller to work with articles data.
    /// </summary>
    [ApiController]
    [Route("api/article")]
    public class ArticleController : ApiControllerBase
    {
        private readonly IArticleService articleService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="articleService">The <see cref="IArticleService"/> implementation.</param>
        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        /// <summary>
        /// Get all Articles asynchronous.
        /// </summary>
        /// <returns>List of all <see cref="Article"/>.</returns>
        [Route("get/all")]
        [HttpGet]
        [AuthorizeApi(Activity.ViewArticles)]
        public async Task<IEnumerable<ArticleDto>> GetAll()
        {
            var result = await articleService.GetAllArticleDtoAsync();
            return result;
        }

        /// <summary>
        /// Insert Article in Database asynchronous.
        /// </summary>
        /// <param name="articleDto">Article.</param>
        [Route("create")]
        [HttpPost]
        [AuthorizeApi(Activity.ManageArticles)]
        public async Task<SimpleResponseDto> CreateArticlesAsync(ArticleDto articleDto)
        {
            await articleService.CreateArticleAsync(articleDto);

            return new SimpleResponseDto();
        }

        /// <summary>
        /// Update the Article with given Id asynchronous.
        /// </summary>
        [Route("update/{id}")]
        [HttpPost]
        [AuthorizeApi(Activity.ManageArticles)]
        public async Task UpdateArticleAsync(long id, [FromBody] ArticleDto articleDto)
        {
            await articleService.UpdateArticleAsync(id, articleDto);
        }

        /// <summary>
        /// Delete Article with given Id asynchronous.
        /// </summary>
        /// <returns>Simple response with status 200.</returns>
        [Route("delete/{id:long}")]
        [HttpPost]
        [AuthorizeApi(Activity.ManageArticles)]
        public async Task<SimpleResponseDto> DeleteArticleAsync(long id)
        {
            await articleService.DeleteArticleAsync(id);

            return new SimpleResponseDto();
        }

        /// <summary>
        /// Search Articles by given filter. Return ordered and paginated list of Articles.
        /// </summary>
        /// <returns>Simple response with status 200.</returns>
        [Route("search")]
        [HttpPost]
        [AuthorizeApi(Activity.ViewArticles)]
        public async Task<IEnumerable<ArticleDto>> SearchAsync(SearchRequestDto<SearchFilterDto> searchRequestDto)
        {
            return await articleService.SearchAsync(searchRequestDto.Filter, searchRequestDto.Order, searchRequestDto.PageNumber, searchRequestDto.ItemsPerPage);
        }
    }
}
