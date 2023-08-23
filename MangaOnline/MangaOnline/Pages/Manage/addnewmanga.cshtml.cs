using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Manage
{
    public class addnewmangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context _context;

        public List<Author> authors { get; set; }
        public List<Category> categories { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public addnewmangaModel(MangaOnlineV1DevPRN221Context context)
        { 
            _context = context;
            categories = _context.Categories.ToList();
            authors = _context.Authors.ToList();
        }
        public void OnGet()
        {
            //authors = _context.Authors.ToList();
            //categories = _context.Categories.ToList();
        }
    }
}
