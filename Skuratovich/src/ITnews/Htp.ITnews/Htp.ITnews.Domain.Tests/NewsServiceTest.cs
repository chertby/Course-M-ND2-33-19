using System;
using System.Collections.Generic;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Domain.Services;
using Moq;
using Xunit;

namespace Htp.ITnews.Domain.Tests
{
    public class NewsServiceTest
    {
        private readonly Mock<INewsRepository> newsRepositoryMock;

        private readonly Mock<IMapper> mapperMock;

        private readonly Mock<IUnitOfWork> unitOfWorkMock;

        private readonly INewsService newsService;

        public NewsServiceTest()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);

            newsRepositoryMock = mockRepo.Create<INewsRepository>();

            newsRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<News>());

            newsRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new News());

            mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<NewsViewModel>(It.IsAny<News>()))
                .Returns(new NewsViewModel());

            mapperMock
                .Setup(x => x.Map<IEnumerable<NewsViewModel>>(It.IsAny<IEnumerable<News>>()))
                .Returns(new List<NewsViewModel>());

            unitOfWorkMock = new Mock<IUnitOfWork>();

            newsService = new NewsService(
                newsRepositoryMock.Object,
                unitOfWorkMock.Object,
                mapperMock.Object);
        }

        [Fact]
        public void GetNewsById_not_null()
        {
            // Arange
            var id = new Guid();

            // Act
            var news = newsService.GetAsync(id).Result;

            // Assert
            Assert.NotNull(news);
        }

        //[Fact]
        //public void GetAllNews_not_null()
        //{
        //    // Arange
         
        //    // Act
        //    var news = newsService.GetAll().Result;

        //    // Assert
        //    Assert.NotNull(news);
        //}
    }
}
