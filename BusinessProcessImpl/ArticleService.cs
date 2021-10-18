using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessProcess;
using Common.Search;
using DataAccess;
using DataAccess.Dto.Article;
using DataAccess.Repositories;
using Model.Entities;

namespace BusinessProcessImpl
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="articleRepository">The implementation of <see cref="IArticleRepository"/>.</param>
        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            this.articleRepository = articleRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateArticleAsync(ArticleDto articleDto)
        {
            var article = articleDto.ToEntity();
            await unitOfWork.AddAsync<Article>(article);
            await unitOfWork.SaveChangesAsync();
        }

        /// </<inheritdoc/>
        public async Task DeleteArticleAsync(long id)
        {
            var entity = await articleRepository.FindByIdAsync<Article>(id);

            if (entity == null)
            {
                throw new ArgumentException($"Article with id {id} not found!");
            }

            unitOfWork.Delete<Article>(entity);
            await unitOfWork.SaveChangesAsync();
        }

        /// </<inheritdoc/>
        public async Task<IEnumerable<ArticleDto>> GetAllArticleDtoAsync()
        {
            return await articleRepository.GetAllArticleDtoAsync();
        }

        /// </<inheritdoc/>
        public async Task<IEnumerable<ArticleDto>> SearchAsync(SearchFilterDto filter, List<SearchOrderDto> order, int pageNumber, int itemsPerPage)
        {
            return await articleRepository.SearchAsync(filter, order, pageNumber, itemsPerPage);
        }

        /// </<inheritdoc/>
        public async Task UpdateArticleAsync(long id, ArticleDto articleDto)
        {
            var article = articleRepository.FindById<Article>(id);

            if (article == null)
            {
                throw new ArgumentException($"Article with id = {id} does not exist");
            }

            article.Body = articleDto.Body;
            article.Title = articleDto.Title;
            unitOfWork.Update<Article>(article);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
