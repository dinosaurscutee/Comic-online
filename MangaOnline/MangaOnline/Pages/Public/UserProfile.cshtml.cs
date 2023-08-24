using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.ModelCore;
using MangaOnline.Models;
using MangaOnline.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MangaOnline.Pages.Public
{
    public class UserProfileModel : AbstractModel
    {
        private readonly MangaOnlineV1DevPRN221Context _mangaOnlineV1DevContext;
        private readonly ILogicHandler _logicHandler;

        public UserProfileModel(MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext,
            ILogicHandler iLogicHandler)
        {
            _mangaOnlineV1DevContext = mangaOnlineV1DevContext;
            _logicHandler = iLogicHandler;
        }

        [BindProperty] public RequestUserProfile RequestUser { get; set; }

        public string? NotificationUpdateUser { get; set; }

        public User? UserProfile1 { get; set; }

        public IActionResult OnGet(string? notilistmanga)
        {
            if (!CheckRoleUser(new[] { UserRoleEnum.Admin.ToString(), UserRoleEnum.NormalUser.ToString(), UserRoleEnum.VipUser.ToString() }))
                return RedirectToPage("/Error");

            if (notilistmanga == "UpdateSuccess")
            {
                NotificationUpdateUser = "Cập nhật thông tin cá nhân thành công";
            }

            var accessToken = HttpContext.Request.Cookies["USER_DATA"];
            UserCookie? userProfile = new UserCookie();
            var avatarLink = "/lib/img/avatar-default.jpg";
            if (accessToken is not null)
            {
                userProfile = JsonConvert.DeserializeObject<UserCookie>(accessToken);
            }
            else
            {
                userProfile = null;
            }

            UserProfile1 = _mangaOnlineV1DevContext.Users
                .Include(x => x.UserToken)
                .FirstOrDefault(x => x.Id == userProfile!.Id);
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _mangaOnlineV1DevContext.Users
                .Include(x => x.UserRole)
                .ThenInclude(x => x.Role)
                .FirstOrDefault(x => x.Id == RequestUser.UserId);
            if (RequestUser.Avarta is null && string.IsNullOrEmpty(user!.Avatar))
            {
            }
            else if (RequestUser.Avarta is not null)
            {
                user!.Avatar = _logicHandler.UpdateImageAvatarUser(RequestUser.Avarta, user.Avatar);
            }

            user!.FullName = RequestUser.FullName;
            user.PhoneNumber = RequestUser.Phone;
            _mangaOnlineV1DevContext.SaveChanges();

            var token = AuthenticationPage.WriteToken(user.FullName, user.Email, user.UserRole.Role.Name);
            var userCookie = new UserCookie()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.UserRole.Role.Name,
                PhoneNumber = user.PhoneNumber,
                Avartar = user.Avatar
            };
            var jsonStr = JsonConvert.SerializeObject(userCookie);
            Response.Cookies.Append("ACCESS_TOKEN", token);
            Response.Cookies.Append("USER_DATA", jsonStr);
            return RedirectToPage("/public/userprofile", new { notilistmanga = "UpdateSuccess" });
        }
    }

    public class RequestUserProfile
    {
        [Required] public Guid? UserId { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public IFormFile? Avarta { get; set; }
    }
}
