using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public Task<IEnumerable<TagViewModel>> GetTagsByTermAsync(string term)
        {
            var tags = tagRepository.GetTagsByTerm(term);
            var result = mapper.Map<IEnumerable<TagViewModel>>(tags);
            return Task.FromResult(result);
        }
    }
}
