using MangaOnline.Enum;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaOnline.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
        private readonly MangaOnlineV1DevPRN221Context _context;

        public IndexModel(ILogger<IndexModel> logger,MangaOnlineV1DevPRN221Context context)
        {
            _logger = logger;
            _context = context;
        }
        
        public List<Manga> TopMonthManga { get; set; }
        public List<Manga> NewUpdateMangas { get; set; }
        public List<Manga> NewDoneMangas { get; set; }
        public List<Manga> TopViewMangas { get; set; }

        public IActionResult OnGet()
        {
            // var t = _mangaOnlineV1DevContext.UserTokens
            //     .FirstOrDefault();
            // if (t is not null)
            // {
            //     Console.WriteLine("The time is now {0}", t.Expires.ToString("yyyy-MM-dd"));
            // }
            
            TopMonthManga = _context.Mangas
                .Include(x=>x.CategoryMangas)
                .ThenInclude(x=>x.Category)
                .Where(x => x.ModifiedAt!.Value.Month == DateTime.Now.Month
                            && x.ModifiedAt!.Value.Year == DateTime.Now.Year)
                .OrderByDescending(x => x.Star).Skip(0).Take(8).ToList();
            NewUpdateMangas = _context.Mangas
                .Include(x=>x.CategoryMangas)
                .ThenInclude(x=>x.Category)
                .Include(x=>x.Chapteres)
                .OrderByDescending(x => x.ModifiedAt).Skip(0).Take(12).ToList();
            NewDoneMangas = _context.Mangas
                .Include(x=>x.CategoryMangas)
                .ThenInclude(x=>x.Category)
                .Include(x=>x.Chapteres)
                .Where(x=>x.Status == (int)MangaStatus.Done)
                .OrderByDescending(x => x.ModifiedAt).Skip(0).Take(12).ToList();
            TopViewMangas = _context.Mangas
                .Include(x=>x.CategoryMangas)
                .ThenInclude(x=>x.Category)
                .Include(x=>x.Chapteres)
                .OrderByDescending(x => x.ViewCount).Skip(0).Take(6).ToList();

            if (TopMonthManga.Count<=4)
            {
                TopMonthManga = _context.Mangas
                    .Include(x=>x.CategoryMangas)
                    .ThenInclude(x=>x.Category)
                    .Where(x => x.ModifiedAt!.Value.Year == DateTime.Now.Year)
                    .OrderByDescending(x => x.Star).Skip(0).Take(8).ToList();
            }
            
            return Page();
        }
}