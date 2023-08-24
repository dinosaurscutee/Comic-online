using MangaOnline.Extensions;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MangaOnline.Pages.Manage
{
    public class addnewmangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context _context;
        private readonly ILogicHandler _logicHandler;

        [BindProperty] 
        public RequestAddManga RequestAddManga { get; set; }

        [BindProperty]
        public List<Author> authors { get; set; }

        [BindProperty]
        public List<Guid> CategoriesId { get; set; }

        [BindProperty]
        public string description { get; set; }

        public List<Category> categories { get; set; }
        public string title { get; set; }
        
        public Guid? MangaId { get; set; }

        public Manga? MangaUpdate { get; set; } = default;
        public addnewmangaModel(MangaOnlineV1DevPRN221Context context, ILogicHandler iLogicHandler)
        { 
            _context = context;
            _logicHandler = iLogicHandler;
        }
        public IActionResult OnGet(Guid? mangaId)
        {
            authors = _context.Authors.ToList();
            categories = _context.Categories.ToList();
            var mangaOld = _context.Mangas
            .Include(x => x.Author)
            .Include(x => x.CategoryMangas)
            .FirstOrDefault(x => x.Id == mangaId);
            if (mangaOld is not null)
            {
                MangaId = mangaId;
                MangaUpdate = mangaOld;
                description = mangaOld.Description;
            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            categories = _context.Categories.ToList();
            if (RequestAddManga.MangaId is null)
            {
                // add Manga
                var author = new Author()
                {
                    Id = Guid.NewGuid(),
                    Name = RequestAddManga.AuthorName
                };
                _context.Authors.Add(author);
                DateTime utcTime1 = new DateTime(RequestAddManga.CreatedAt, 1, 1);
                utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
                DateTimeOffset utcTime2 = utcTime1;
                var manga = new Manga()
                {
                    Id = Guid.NewGuid(),
                    Name = RequestAddManga.Name,
                    AuthorId = author.Id,
                    ViewCount = 0,
                    RateCount = 0,
                    Star = 0,
                    FollowCount = 0,
                    Description = description,
                    CreatedAt = utcTime2,
                    ModifiedAt = DateTimeOffset.Now,
                    IsActive = false,
                    Image = _logicHandler.CreateImage(RequestAddManga.Image)
                };
                if (RequestAddManga.Status.Equals("Hoàn thành"))
                {
                    manga.Status = (int)MangaStatus.Done;
                }
                else if (RequestAddManga.Status.Equals("Đang cập nhật"))
                {
                    manga.Status = (int)MangaStatus.Updating;
                }
                else
                {
                    manga.Status = (int)MangaStatus.StopUpdating;
                }
                foreach (var categoryId in CategoriesId)
                {
                    _context.CategoryMangas
                        .Add(new CategoryManga() { CategoryId = categoryId, MangaId = manga.Id });
                }
                _context.Mangas.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Manage/listmanga", new { notilistmanga = "AddSuccess" });
            }
            else
            {
                var categoryMangasOld =
                    _context.CategoryMangas
                        .Where(x => x.MangaId == RequestAddManga.MangaId);
                _context.CategoryMangas.RemoveRange(categoryMangasOld);
                var mangaOld =
                    await _context.Mangas
                        .Include(x => x.Author)
                        .FirstOrDefaultAsync(x => x.Id == RequestAddManga.MangaId);
                if (mangaOld is not null)
                {
                    mangaOld.Name = RequestAddManga.Name;
                    //mangaOld.Author.Name = RequestAddManga.AuthorName;
                    mangaOld.Author.Name = RequestAddManga.AuthorName;
                    mangaOld.Description = description;
                    foreach (var categoryId in CategoriesId)
                    {
                        _context.CategoryMangas
                            .Add(new CategoryManga() { CategoryId = categoryId, MangaId = mangaOld.Id });
                    }
                    DateTime utcTime1 = new DateTime(RequestAddManga.CreatedAt, 1, 1);
                    utcTime1 = DateTime.SpecifyKind(utcTime1, DateTimeKind.Utc);
                    DateTimeOffset utcTime2 = utcTime1;
                    mangaOld.CreatedAt = utcTime2;
                    mangaOld.ModifiedAt = DateTimeOffset.Now;
                    mangaOld.Image =
                        RequestAddManga.Image is null ?
                            mangaOld.Image :
                            _logicHandler.UpdateImage(RequestAddManga.Image!, mangaOld.Image!);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Manage/listmanga", new { notilistmanga = "UpdateSuccess" });
                }
            }
            return Page();
        }
    }
}
public class RequestAddManga
{
    [Required]
    public Guid? MangaId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string AuthorName { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CreatedAt { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public Guid[] CategoriesId { get; set; }
    [Required]
    public IFormFile? Image { get; set; }
}

public enum MangaStatus
{
    Done, 
    Updating, 
    StopUpdating, 
}
