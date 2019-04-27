using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.newsRepository = newsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NewsViewModel>> GetAllAsync()
        {
            var news = await newsRepository.GetAllAsync();
            var result = mapper.Map<IEnumerable<NewsViewModel>>(news);
            return result;
        }

        public async Task<NewsViewModel> GetAsync(Guid id)
        {
            var news = await newsRepository.GetAsync(id);
            var result = mapper.Map<NewsViewModel>(news);
            return result;
        }
    }
}
