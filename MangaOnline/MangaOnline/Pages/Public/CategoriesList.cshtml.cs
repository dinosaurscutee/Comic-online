using MangaOnline.Enum;
using MangaOnline.Models;
using MangaOnline.Pages.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Public;

public class CategoriesList : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;

    public List<Manga> listManga = new();
    public Dictionary<Guid, List<Category>> mangaCategoryDict = new();
    public List<Category> Categories = new();

    public string Genre { get; set; }
    public string Status { get; set; }
    public string Sort { get; set; }
    public string Rating { get; set; }

    public int LastPage { get; set; }
    public int PageSize { get; set; } = 12;
    public int PageIndex { get; set; } = 1;

    public CategoriesList(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }

    public void OnGet(string? cateName, string genre, string status, string sort, string rating, int index)
    {
        var mList = _context.Mangas.ToList();
        var cmList = _context.CategoryMangas.ToList();
        var cList = _context.Categories.ToList();

        if (!string.IsNullOrEmpty(cateName))
        {
            Genre = cateName;
        }
        else
        {
            Genre = string.IsNullOrEmpty(genre) ? "Tất cả thể loại" : genre;
        }

        Status = string.IsNullOrEmpty(status) ? "Tất cả" : status;
        Sort = string.IsNullOrEmpty(sort) ? "Ngày cập nhật" : sort;
        Rating = string.IsNullOrEmpty(rating) ? "Tất cả" : rating;

        var statusEnum = Status switch
        {
            "Hoàn thành" => (int)MangaStatusEnum.Done,
            "Đang cập nhật" => (int)MangaStatusEnum.Updating,
            "Dừng cập nhật" => (int)MangaStatusEnum.StopUpdating,
            _ => -1
        };

        var strSort = Sort switch
        {
            "Ngày cập nhật" => "ModifiedAt",
            "Top view" => "ViewCount",
            "Top followers" => "FollowCount",
            _ => "ModifiedAt"
        };

        if (!Genre.Equals("Tất cả thể loại"))
        {
            listManga = (from m in mList
                join cm in cmList
                    on m.Id equals cm.MangaId
                join c in cList
                    on cm.CategoryId equals c.Id
                where c.Name == Genre
                select m).ToList();
        }
        else
        {
            listManga = _context.Mangas.OrderBy(x => x.ModifiedAt).ToList();
        }

        if (statusEnum != -1)
        {
            listManga = listManga.Where(x => x.Status == statusEnum).ToList();
        }

        if (!Rating.Equals("Tất cả"))
        {
            var starInt = int.Parse(rating);
            listManga = listManga.Where(x => x.Star == starInt).ToList();
        }

        LastPage = listManga.Count / PageSize;
        if (listManga.Count % PageSize > 0)
        {
            LastPage += 1;
        }

        index = index == 0 ? 1 : index;
        PageIndex = index;
        if (index <= LastPage)
        {
            listManga = listManga.Skip(PageSize * (index - 1)).Take(PageSize).ToList();
        }

        listManga = (from m in listManga orderby strSort select m).ToList();

        foreach (var manga in listManga)
        {
            var cateResult = from c in cList
                join cm in cmList
                    on c.Id equals cm.CategoryId
                join m in mList
                    on cm.MangaId equals m.Id
                where m.Id == manga.Id
                select c;
            mangaCategoryDict.Add(manga.Id, cateResult.ToList());
        }

        Categories = _context.Categories.ToList();
    }
}