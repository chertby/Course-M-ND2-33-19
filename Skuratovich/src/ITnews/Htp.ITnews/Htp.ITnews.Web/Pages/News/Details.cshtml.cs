﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsService newsService;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }

        public DetailsModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsViewModel = await newsService.GetAsync(id.GetValueOrDefault(), User.GetUserId());

            if (NewsViewModel == null)
            {
                return NotFound();
            }

            NewsViewModel.StringTags = string.Join(",", NewsViewModel.Tags.Select(t => t.Title));

            return Page();
        }

        [Authorize(Policy = "RequireRole")]
        public async Task<IActionResult> OnPostRateAsync(Guid id, int value)
        {
            await newsService.RateAsync(id, User.GetUserId(), value);
            return new JsonResult("ok");
        }
    }
}