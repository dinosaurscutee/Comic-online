using MangaOnline.Enum;
using MangaOnline.ModelCore;
using MangaOnline.Models;
using MangaOnline.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Public;

public class DetailChapter : AbstractModel
{
    private readonly MangaOnlineV1DevPRN221Context _mangaOnlineV1DevContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DetailChapter(MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext)
    {
        _mangaOnlineV1DevContext = mangaOnlineV1DevContext;
    }
    public string Chaptere { get; set; }
    public IActionResult OnGet(Guid id)
    {
        var chapter = _mangaOnlineV1DevContext.Chapteres.FirstOrDefault(x => x.Id == id);
        if (chapter != null)
        {
            var chapterByManga = new ChapterByManga()
            {
                ChapterId = chapter.Id,
                MangaId = chapter.MangaId
            };
            if (chapter.Status==(int)StatusChapterEnum.ChapterVip)
            {
                if (!CheckRoleUser(new[] { UserRoleEnum.VipUser.ToString(), UserRoleEnum.Admin.ToString() }))
                    return RedirectToPage("/Error");
            }
            var listChapter = Request.Cookies["LIST_CHAPTER"];
            if (listChapter != null)
            {
                var listChapterByManga = JsonConvert.DeserializeObject<List<ChapterByManga>>(listChapter);
                if (listChapterByManga != null)
                {
                    var chapterByMangaOld = listChapterByManga.FirstOrDefault(x => x.MangaId == chapterByManga.MangaId);
                    if (chapterByMangaOld != null)
                    {
                            listChapterByManga.Remove(chapterByMangaOld);
                    }
                    listChapterByManga.Add(chapterByManga);
                }
                else
                {
                    listChapterByManga = new List<ChapterByManga>();
                    listChapterByManga.Add(chapterByManga);
                }
                Response.Cookies.Append("LIST_CHAPTER", JsonConvert.SerializeObject(listChapterByManga));
            }
            else
            {
                var listChapterByManga = new List<ChapterByManga>(){chapterByManga};
                Response.Cookies.Append("LIST_CHAPTER", JsonConvert.SerializeObject(listChapterByManga));
            }

            Chaptere = chapter.FilePdf!;
            return Page();
        }
        else
        {
            return RedirectToPage("/Error");
        }
    }
}