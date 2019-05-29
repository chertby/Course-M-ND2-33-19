using System;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TagViewModel> GetAsync(Guid id)
        {
            var tag = await unitOfWork.Repository<Tag>()
                .GetAsync(id, x => x);

            var result = mapper.Map<TagViewModel>(tag);
            return result;
        }

        public Task<IEnumerable<TagViewModel>> GetTagsByTermAsync(string term)
        {
            var tags = unitOfWork.Repository<Tag>()
                .FindByCondition(t => t.Title.Contains(term))
                .Take(10)
                .ToList();
            var result = mapper.Map<IEnumerable<TagViewModel>>(tags);
            return Task.FromResult(result);
        }

        public IEnumerable<TagForCloudViewModel> GetTagsForCloud()
        {
            var tags = unitOfWork.Repository<Tag>()
                .GetAll(x => x
                .Include(t => t.NewsTags));

            var result = tags
                .Select(t => new TagForCloudViewModel() { Id = t.Id, Title = t.Title, Count = t.NewsTags.Count })
                .Where(t => t.Count > 0)
                .OrderByDescending(t => t.Count)
                .ToArray();

            return result;
        }
    }
}
