using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Htp.ITnews.Web.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsService newsService;

        public IList<NewsViewModel> News { get; set; }

        public IndexModel(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public async Task OnGetAsync()
        {
            var result = await newsService.GetAllAsync();
            News = (List<NewsViewModel>)result;
        }
    }
}
