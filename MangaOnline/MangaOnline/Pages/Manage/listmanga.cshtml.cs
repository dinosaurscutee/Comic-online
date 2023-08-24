using MangaOnline.Extensions;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaOnline.Pages.Manage
{
    public class listmangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public listmangaModel(IJwtTokenHandler jwtTokenHandler,
            MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext,
            IHttpContextAccessor iHttpContextAccessor) 
        {
            _context = mangaOnlineV1DevContext;
            _httpContextAccessor = iHttpContextAccessor;
        }

        public string? NotificationListManga { get; set; }
        public List<Manga> ListManga { get; set; }
        public List<Category> CategoryList { get; set; }

        public string Genre { get; set; }
        public string Status { get; set; }
        public string StatusOff { get; set; }
        public string Sort { get; set; }

        public int LastPage { get; set; }
        public int PageSize { get; set; } = 6;
        public int PageIndex { get; set; } = 1;

        public IActionResult OnGet(string? notiListManga, string? genre, string? status, string? statusOff, string? sort, int index)
        {
            // var c1 = Request.Url.AbsoluteUri;
            CategoryList = _context.Categories.ToList();
            Genre = string.IsNullOrEmpty(genre) ? "Tất cả" : genre;
            Status = string.IsNullOrEmpty(status) ? "Tất cả" : status;
            StatusOff = string.IsNullOrEmpty(statusOff) ? "Tất cả" : statusOff;
            Sort = string.IsNullOrEmpty(sort) ? "Tất cả" : sort;
            // return Page();
            //if (!CheckRoleUser(new[] { "Admin" }, false))
            //{
            //    return RedirectToPage("/Error");
            //}

            if (notiListManga == "UpdateSuccess")
            {
                NotificationListManga = "Cập nhật truyện thành công";
            }

            if (notiListManga == "AddSuccess")
            {
                NotificationListManga = "Tạo truyện thành công";
            }

            var mList = _context.Mangas.ToList();
            var cmList = _context.CategoryMangas.ToList();
            var auList = _context.Authors.ToList();
            if (Genre != "Tất cả")
            {
                ListManga = (from m in mList
                             join cm in cmList
                                 on m.Id equals cm.MangaId
                             join c in CategoryList
                                 on cm.CategoryId equals c.Id
                             join au in auList
                                 on m.AuthorId equals au.Id
                             where c.Name == Genre
                             select m).ToList();
            }
            else
            {
                ListManga = _context.Mangas
                    .Include(x => x.Author)
                    .Include(x => x.CategoryMangas)
                    .ThenInclude(x => x.Category)
                    .OrderBy(x => x.ModifiedAt).ToList();
            }

            var statusEnum = Status switch
            {
                "Hoàn thành" => (int)MangaStatus.Done,
                "Đang cập nhật" => (int)MangaStatus.Updating,
                "Dừng cập nhật" => (int)MangaStatus.StopUpdating,
                _ => -1
            };
            bool? statusOffEnum = StatusOff switch
            {
                "Đang ẩn" => false,
                "Đang hiện" => true,
                _ => null
            };
            if (Status != "Tất cả")
            {
                ListManga = ListManga.Where(x => x.Status == statusEnum).ToList();
            }
            if (statusOffEnum is not null)
            {
                ListManga = ListManga.Where(x => x.IsActive == statusOffEnum).ToList();
            }

            switch (Sort)
            {
                case "Lượt xem cao":
                    ListManga = ListManga.OrderByDescending(x => x.ViewCount).ToList();
                    break;
                case "Lượt đánh giá cao":
                    ListManga = ListManga.OrderByDescending(x => x.Star).ToList();
                    break;
                case "Lượt theo dõi cao":
                    ListManga = ListManga.OrderByDescending(x => x.FollowCount).ToList();
                    break;
            }

            LastPage = ListManga.Count / PageSize;
            if (ListManga.Count % PageSize > 0)
            {
                LastPage += 1;
            }
            index = index == 0 ? 1 : index;
            PageIndex = index;
            if (index <= LastPage)
            {
                ListManga = ListManga.Skip(PageSize * (index - 1)).Take(PageSize).ToList();
            }
            return Page();
        }

        public IActionResult OnPostChangeIsActive(Guid mangaId)
        {
            var user = _context.Mangas.FirstOrDefault(x => x.Id == mangaId);

            user!.IsActive = !user.IsActive;
            _context.SaveChanges();
            return RedirectToPage("/manage/listmanga");
        }
    }
}