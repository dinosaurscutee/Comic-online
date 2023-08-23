using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Manage
{
    public class listmangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context _context;

        public List<Manga> manga { get; set; }

        public listmangaModel(MangaOnlineV1DevPRN221Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            manga = _context.Mangas.ToList();
        }

        //public IActionResult OnPostSearch(string searchTerm)
        //{
        //    if (searchTerm != null || searchTerm != "")
        //    {
        //        //manga = _context.Mangas.Where(x => x.Contains(searchTerm)).ToList();
        //        manga = _context.Mangas.ToList();
        //    }
        //    else
        //    {
        //        manga = _context.Mangas.ToList();
        //    }
            

        //    return Page();
        //}
    }
}
