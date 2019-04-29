﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Domain.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<NewsViewModel>> GetAllAsync();
        Task<NewsViewModel> GetAsync(Guid id);
        Task<NewsViewModel> AddAsync(NewsViewModel newsViewModel);
        Task<NewsViewModel> EditAsync(NewsViewModel newsViewModel);

        Task<List<SelectListItem>> GetСategoriesAsync();
    }
}
