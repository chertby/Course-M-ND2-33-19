using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Htp.ITnews.Data.Contracts;
using Htp.ITnews.Data.Contracts.Entities;
using Htp.ITnews.Data.EntityFramework.Extensions;
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
            var tags = unitOfWork.Repository<Tag>()
                .FindByCondition(t => t.Title.Contains(term))
                .ToList();
            var result = mapper.Map<IEnumerable<TagViewModel>>(tags);
            return Task.FromResult(result);
        }

        public async Task<int> Test()
        {
            var test = await unitOfWork.Repository<Tag>()
                .GetAllAsync(x => x
                .Include(t => t.NewsTags));

            //test.GroupBy(t => t.Id).See

            return test.Count();

            //unitOfWork.Repository<Tag>()
            //var tags = unitOfWork.Repository<Tag>().GetAllAsync().In

        }
    }
}
