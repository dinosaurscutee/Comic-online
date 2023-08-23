using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Public
{
    public class DetailMangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context db;
        public Manga manga = new Manga();
        public bool isFollowed = false;
        public List<Manga> ListManga = new List<Manga>();
        public Dictionary<Guid, List<Category>> mangaCategoryDict = new Dictionary<Guid, List<Category>>();
        public List<Comment> ListComment = new List<Comment>();
        public Comment comment = new Comment();

        private UserCookie user = new UserCookie();

        public DetailMangaModel(MangaOnlineV1DevPRN221Context db)
        {
            this.db = db;
        }

        public UserCookie GetUser()
        {
            var accessToken = HttpContext.Request.Cookies["USER_DATA"];
            if (accessToken is not null)
            {
                user = JsonConvert.DeserializeObject<UserCookie>(accessToken);
            }
            else
            {
                user = null;
            }

            return user;
        }

        public void OnGet(string id)
        {
            user = GetUser();
            manga = db.Mangas.FirstOrDefault(x => x.Id == Guid.Parse(id));

            var author = db.Authors.FirstOrDefault(x => x.Id == manga.AuthorId);
            manga.Author = author;

            var chapters = db.Chapteres.Where(x => x.MangaId == manga.Id).OrderBy(x=>x.ChapterNumber).ToList();
            manga.Chapteres = chapters;

            var mList = db.Mangas.ToList();
            ListManga = mList.Where(x => x.Id != Guid.Parse(id)).ToList();

            var cmList = db.CategoryMangas.ToList();
            var cList = db.Categories.ToList();

            foreach (var ma in mList)
            {
                var cateResult = from c in cList
                    join cm in cmList
                        on c.Id equals cm.CategoryId
                    join m in mList
                        on cm.MangaId equals m.Id
                    where m.Id == ma.Id
                    select c;
                mangaCategoryDict.Add(ma.Id, cateResult.ToList());
            }

            if (user != null)
            {
                var followManga = db.FollowLists.FirstOrDefault(x => x.UserId == user.Id && x.MangaId == manga.Id);
                if (followManga != null)
                {
                    isFollowed = true;
                }
            }
            else
            {
                isFollowed = false;
            }

            manga.ViewCount++;
            db.SaveChanges();

            ListComment = db.Comments
                .Include(x => x.Manga)
                .Include(x => x.User)
                .Where(x => x.MangaId == manga.Id && x.IsActive == true).ToList();
        }

        public IActionResult OnPostFollow(string mangaId)
        {
            user = GetUser();
            manga = db.Mangas.FirstOrDefault(x => x.Id == Guid.Parse(mangaId));

            var followManga = new FollowList()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                MangaId = manga.Id
            };
            
            // add list mangaFollowId on cookie
            var listMangaIdFollow = db.FollowLists
                .Where(x => x.UserId == user.Id)
                .Select(x=>x.MangaId).ToList();
            if (listMangaIdFollow.Count>0)
            {
                var followStr = "";
                foreach (var fId in listMangaIdFollow)
                {
                    followStr += fId;
                    if (!fId.Equals(listMangaIdFollow[^1]))
                    {
                        followStr += ",";
                    }
                }
                Response.Cookies.Append("MANGA_FOLLOW", followStr);
            }
            // add list mangaFollowId on cookie
            
            db.FollowLists.Add(followManga);
            db.SaveChanges();

            manga.FollowCount++;
            db.SaveChanges();

            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostUnfollow(string mangaId)
        {
            user = GetUser();
            manga = db.Mangas.FirstOrDefault(x => x.Id == Guid.Parse(mangaId));

            var followManga = db.FollowLists.FirstOrDefault(x => x.UserId == user.Id && x.MangaId == manga.Id);
            // add list mangaFollowId on cookie
            var listMangaIdFollow = db.FollowLists
                .Where(x => x.UserId == user.Id)
                .Select(x=>x.MangaId).ToList();
            if (listMangaIdFollow.Count>0)
            {
                var followStr = "";
                foreach (var fId in listMangaIdFollow)
                {
                    followStr += fId;
                    if (!fId.Equals(listMangaIdFollow[^1]))
                    {
                        followStr += ",";
                    }
                }
                Response.Cookies.Append("MANGA_FOLLOW", followStr);
            }
            // add list mangaFollowId on cookie
            db.FollowLists.Remove(followManga);
            db.SaveChanges();

            manga.FollowCount--;
            db.SaveChanges();

            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostLikeComment(string commentId, string mangaId)
        {
            var commentObj = db.Comments.FirstOrDefault(x => x.Id == Guid.Parse(commentId));
            commentObj.LikedCount++;
            db.SaveChanges();
            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostUnlikeComment(string commentId, string mangaId)
        {
            var commentObj = db.Comments.FirstOrDefault(x => x.Id == Guid.Parse(commentId));
            commentObj.DislikedCount++;
            db.SaveChanges();
            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostAddComment(string comment, string mangaId)
        {
            user = GetUser();
            var newComment = new Comment()
            {
                Id = Guid.NewGuid(),
                MangaId = Guid.Parse(mangaId),
                UserId = user.Id,
                Content = comment,

                CreatedAt = DateTimeOffset.Now,
                LikedCount = 0,
                DislikedCount = 0,
                IsActive = true
            };
            db.Comments.Add(newComment);
            db.SaveChanges();
            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostReadContinues(string mangaId)
        {
            var listChapter = Request.Cookies["LIST_CHAPTER"];
            if (listChapter != null)
            {
                var listChapterId = JsonConvert.DeserializeObject<List<ChapterByManga>>(listChapter);
                var chapter = listChapterId?.FirstOrDefault(x => x.MangaId.ToString() == mangaId);
                if (chapter != null)
                {
                    var chapterId = chapter.ChapterId;
                    var chapterWantTo = db.Chapteres.FirstOrDefault(x => x.Id == chapterId);
                    if (chapterWantTo != null)
                    {
                        return RedirectToPage("/Public/DetailChapter", new { id = chapterWantTo.Id });
                    }
                }
            }

            var chapterFirst = db.Chapteres.Where(x => x.MangaId.ToString() == mangaId)
                .OrderBy(x => x.ChapterNumber)
                .FirstOrDefault();
            if (chapterFirst != null)
            {
                return RedirectToPage("/Public/DetailChapter", new { id = chapterFirst.Id });
            }

            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }

        public IActionResult OnPostFirstChapter(string mangaId)
        {
            var chapterFirst = db.Chapteres.Where(x => x.MangaId.ToString() == mangaId)
                .OrderBy(x => x.ChapterNumber)
                .FirstOrDefault();
            if (chapterFirst != null)
            {
                return RedirectToPage("/Public/DetailChapter", new { id = chapterFirst.Id });
            }
            return RedirectToPage("/Public/DetailManga", new { id = mangaId });
        }
    }
}