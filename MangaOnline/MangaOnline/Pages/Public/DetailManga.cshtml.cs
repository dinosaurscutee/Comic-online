using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Public
{
    public class DetailMangaModel : PageModel
    {
        private readonly MangaOnlineV1DevPRN221Context db;
        public Manga manga = new Manga();
		public bool isFollowed = false;
		public List<Manga> mangas = new List<Manga>();
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
        }
    }
}
