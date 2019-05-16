using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Htp.ITnews.Domain.Contracts;
using Htp.ITnews.Domain.Contracts.ViewModels;
using Htp.ITnews.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Htp.ITnews.Web.Pages.News
{
    [Authorize(Roles = "Administrator,Writer")]
    public class CreateModel : PageModel
    {
        private readonly INewsService newsService;
        private readonly ITagService tagService;
        private readonly IHostingEnvironment appEnvironment;

        [BindProperty]
        public NewsViewModel NewsViewModel { get; set; }
        public List<SelectListItem> Сategories { get; private set; }

        public CreateModel(INewsService newsService, ITagService tagService, IHostingEnvironment appEnvironment)
        {
            this.newsService = newsService;
            this.tagService = tagService;
            this.appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewsViewModel.AuthorId = User.GetUserId();

            await newsService.AddAsync(NewsViewModel);

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetTags(string term)
        {
            var tags = await tagService.GetTagsByTermAsync(term);
            return new JsonResult(tags);
        }

        public async Task<IActionResult> OnPostFileAsync(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                var erorr = new FileViewModel() { Erorr = "Error while uploading file" };
                return new JsonResult(erorr);
            }

            var filePath = "/files/img/" + uploadedFile.FileName;

            var filename = appEnvironment.WebRootPath + filePath;
            var fileUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}{filePath}";

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            var file = new FileViewModel() { DownloadUrl = fileUrl };

            return new JsonResult(file);
        }


        private async Task PopulateLists()
        {
            Сategories = await newsService.GetСategoriesAsync();
        }
    }
}