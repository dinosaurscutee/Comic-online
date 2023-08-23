using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.Models;
using MangaOnline.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MangaOnline.Pages.Manage;

public class AddMangaModel : AbstractModel
{
    private readonly MangaOnlineV1DevPRN221Context _mangaOnlineV1DevContext;
    private readonly ILogicHandler _logicHandler;
    public AddMangaModel(
        MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext, ILogicHandler logicHandler
    )
    {
        _mangaOnlineV1DevContext = mangaOnlineV1DevContext;
        _logicHandler = logicHandler;

    }

    [BindProperty] public RequestAddManga RequestAddManga { get; set; }

    [BindProperty] public List<Guid> CategoriesId { get; set; }

    [BindProperty] public string Description { get; set; }

    public List<Category> ListCategory { get; set; }
    public Guid? MangaId { get; set; }

    public Manga? MangaUpdate { get; set; } = default;

    public IActionResult OnGet(Guid? mangaId)
    {

        if (!CheckRoleUser(new[] { UserRoleEnum.Admin.ToString() }))
            return RedirectToPage("/Error");

        ListCategory = _mangaOnlineV1DevContext.Categories.ToList();
        var mangaOld = _mangaOnlineV1DevContext.Mangas
            .Include(x => x.Author)
            .Include(x => x.CategoryMangas)
            .FirstOrDefault(x => x.Id == mangaId);
        if (mangaOld is not null)
        {
            MangaId = mangaId;
            MangaUpdate = mangaOld;
            Description = mangaOld.Description;
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        ListCategory = _mangaOnlineV1DevContext.Categories.ToList();
        if (RequestAddManga.MangaId is null)
        {
            // add Manga
            var author = new Author()
            {
                Id = Guid.NewGuid(),
                Name = RequestAddManga.AuthorName
            };
            _mangaOnlineV1DevContext.Authors.Add(author);
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
                Description = Description,
                CreatedAt = utcTime2,
                ModifiedAt = DateTimeOffset.Now,
                IsActive = RequestAddManga.IsActive,
                Image = _logicHandler.CreateImage(RequestAddManga.Image)
            };
            if (RequestAddManga.Status.Equals("Hoàn thành"))
            {
                manga.Status = (int)MangaStatusEnum.Done;
            }
            else if (RequestAddManga.Status.Equals("Đang cập nhật"))
            {
                manga.Status = (int)MangaStatusEnum.Updating;
            }
            else
            {
                manga.Status = (int)MangaStatusEnum.StopUpdating;
            }
            foreach (var categoryId in CategoriesId)
            {
                _mangaOnlineV1DevContext.CategoryMangas
                    .Add(new CategoryManga() { CategoryId = categoryId, MangaId = manga.Id });
            }
            _mangaOnlineV1DevContext.Mangas.Add(manga);
            await _mangaOnlineV1DevContext.SaveChangesAsync();
            return RedirectToPage("/Manage/ListMangaManager", new { notilistmanga = "AddSuccess" });
        }
        else
        {
            // update manga
            var categoryMangasOld =
                _mangaOnlineV1DevContext.CategoryMangas
                    .Where(x => x.MangaId == RequestAddManga.MangaId);
            _mangaOnlineV1DevContext.CategoryMangas.RemoveRange(categoryMangasOld);
            var mangaOld =
                await _mangaOnlineV1DevContext.Mangas
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Id == RequestAddManga.MangaId);
            if (mangaOld is not null)
            {
                mangaOld.Name = RequestAddManga.Name;
                mangaOld.Author.Name = RequestAddManga.AuthorName;
                mangaOld.Description = Description;
                mangaOld.IsActive = RequestAddManga.IsActive;
                foreach (var categoryId in CategoriesId)
                {
                    _mangaOnlineV1DevContext.CategoryMangas
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
                await _mangaOnlineV1DevContext.SaveChangesAsync();
                return RedirectToPage("/Manage/ListMangaManager", new { notilistmanga = "UpdateSuccess" });
            }
        }
        return Page();
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
