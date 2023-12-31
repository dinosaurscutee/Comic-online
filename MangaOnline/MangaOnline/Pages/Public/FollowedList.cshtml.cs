using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Public
{
    public class FollowedListModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context db;

        public List<Manga> listManga = new List<Manga>();
		public Dictionary<Guid, List<Category>> mangaCategoryDict = new();
		public List<Category> Categories = new();
		public List<FollowList> ListFollow = new();
		private UserCookie user = new UserCookie();

        public FollowedListModel(MangaOnlineV1DevPRN221Context db) 
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

		public IActionResult OnGet()
		{
			Response.Cookies.Delete("NotiFollowStatus");
			user = GetUser();
			if(user == null)
			{
				return RedirectToPage("/Auth/AuthLogin");

            }	
			var totalManga = db.FollowLists.Count();

			ListFollow = db.FollowLists.Where(x => x.UserId == user.Id).ToList();

			listManga = (from m in db.Mangas
						 join f in db.FollowLists
							 on m.Id equals f.MangaId
						 where f.UserId == user.Id
						 select m).ToList();

			foreach (var manga in listManga)
			{
				var cateResult = from c in db.Categories
								 join cm in db.CategoryMangas
									 on c.Id equals cm.CategoryId
								 join m in db.Mangas
									 on cm.MangaId equals m.Id
								 where m.Id == manga.Id
								 select c;
				mangaCategoryDict.Add(manga.Id, cateResult.ToList());
			}
			return Page();
		}

		public IActionResult OnPostUnfollow(string mangaId)
		{
			user = GetUser();
			var manga = db.Mangas.FirstOrDefault(x => x.Id == Guid.Parse(mangaId));

			var followManga = db.FollowLists.FirstOrDefault(x => x.UserId == user.Id && x.MangaId == manga.Id);
			db.FollowLists.Remove(followManga);
			db.SaveChanges();

			manga.FollowCount--;
			db.SaveChanges();
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
			return RedirectToPage("/Public/FollowedList");
		}

	}
}
