using System;
using Autofac.Extras.Moq;
using BusinessProcess;
using DataAccess;
using DataAccess.Dto.Article;
using DataAccess.Repositories;
using Model.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessProcessImpl.Tests
{
    /// <summary>
    /// Tests for <see cref="IArticleService"/>.
    /// </summary>
    [TestFixture]
    public class ArticleServiceTests
    {
        private AutoMock mock;
        private IArticleService articleService;
        private Mock<IArticleRepository> articleRepository;
        private Mock<IUnitOfWork> unitOfWork;

        private Article articleOne = new Article() { Id = 1, Title = "title1" };

        /// <summary>
        /// Initialize mocks.
        /// </summary>
        [SetUp]
        public void Init()
        {
            mock = AutoMock.GetLoose();
            articleService = mock.Create<ArticleService>();
            articleRepository = mock.Mock<IArticleRepository>();
            unitOfWork = mock.Mock<IUnitOfWork>();

            articleRepository.Setup(s => s.FindById<Article>(1)).Returns(articleOne);
        }

        /// <summary>
        /// Dispose mocks.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            mock.Dispose();
        }

        [Test]
        public void Successful_UpdateArticle_Test()
        {
            articleService.UpdateArticleAsync(1, new ArticleDto() { Title = "title updated" });
            unitOfWork.Verify(unit => unit.Update(It.IsAny<Article>()));
            unitOfWork.Verify(unit => unit.SaveChangesAsync());
        }

        /// <summary>
        /// Fail to edit non-exisitng rticle instance.
        /// </summary>
        [Test]
        public void Fail_UpdateNonExistingArticle_Test()
        {
            var articleDto = new ArticleDto();
            var ex = Assert.ThrowsAsync<ArgumentException>(() => articleService.UpdateArticleAsync(6, articleDto));
            Assert.AreEqual("Article with id = 6 does not exist", ex.Message);
        }
    }
}
