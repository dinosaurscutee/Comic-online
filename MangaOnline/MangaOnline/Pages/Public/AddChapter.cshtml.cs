using MangaOnline.Extensions;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Public
{
    public class AddChapterModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context _context;
        private readonly ILogicHandler _logicHandler;
        public AddChapterModel(MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext, ILogicHandler logicHandler)
        {
            _context = mangaOnlineV1DevContext;
            _logicHandler = logicHandler;
        }

        public Guid? MangaId { get; set; }
        public Manga manga { get; set; }

        public IActionResult OnGet(string id)
        {
            //if (!CheckRoleUser(new[] { "Admin" }, false))
            //{
            //	return RedirectToPage("/Error");
            //}
            manga = _context.Mangas.FirstOrDefault(x => x.Id == Guid.Parse(id));
            ViewData["done"] = 0;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            //if (!CheckRoleUser(new[] { "Admin" }, false))
            //{
            //	return RedirectToPage("/Error");
            //}
            IFormFile file = Request.Form.Files.GetFile("fileUp");
            //IFormFile file = Request.Form.Files.GetFile("wpName");

            Chaptere chaptere = new Chaptere();
            chaptere.ChapterNumber = int.Parse(Request.Form["ChapNumber"]);
            chaptere.Id = Guid.NewGuid();
            chaptere.SubId = 0;
            chaptere.MangaId = Guid.Parse(Request.Form["mangaId"]);
            chaptere.Name = " Chapter " + Request.Form["ChapNumber"];
            chaptere.CreatedAt = DateTimeOffset.Now;
            chaptere.Status = 1;
            chaptere.IsActive = true;
            chaptere.FilePdf = _logicHandler.CreatePDF(file);

            _context.Chapteres.AddAsync(chaptere);
            _context.SaveChanges();
            manga = _context.Mangas.FirstOrDefault(x => x.Id == chaptere.MangaId);
            ViewData["done"] = 1;

            return Page();
        }
    }
}
