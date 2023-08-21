using System.ComponentModel.DataAnnotations;
using MangaOnline.Extensions;
using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Auth;

public class AuthLogin : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;

    public AuthLogin(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }

    [BindProperty] public UserLogin UserLogin { get; set; }
    public string MessageEmail { get; set; }
    public string MessagePassword { get; set; }
    public string? NotificationLogin { get; set; }

    public void OnGet(Notilogin? notilogin)
    {
        if (notilogin is not null)
        {
            switch (notilogin)
            {
                case Notilogin.ChangePassword:
                    NotificationLogin = "Đã đổi mật khẩu thành công!";
                    break;
                case Notilogin.Register:
                    NotificationLogin = "Đã gửi email xác thực!";
                    break;
                case Notilogin.RoleUp:
                    NotificationLogin = "Chúc mừng bạn đã thăng hạng!";
                    break;
            }
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _context.Users
            .Include(x => x.UserRole)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == UserLogin.Email);
        if (user is null)
        {
            MessageEmail = "email sai";
            return Page();
        }
        else if (!BCrypt.Net.BCrypt.Verify(UserLogin.Password, user.Password))
        {
            MessagePassword = "password sai";
            return Page();
        }
        else
        {
            var token = AuthenticationPage.WriteToken(user.FullName, user.Email, user.UserRole!.Role.Name!);
            var userCookie = new UserCookie()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.UserRole!.Role.Name!,
                PhoneNumber = user.PhoneNumber ?? "",
                Avartar = user.Avatar
            };

            var jsonStr = JsonConvert.SerializeObject(userCookie);

            if (UserLogin.RememberAccount)
            {
                var cookieOptions1 = new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                };
                Response.Cookies.Append("ACCESS_TOKEN", token, cookieOptions1);
                Response.Cookies.Append("USER_DATA", jsonStr, cookieOptions1);
            }
            else
            {
                Response.Cookies.Append("ACCESS_TOKEN", token);
                Response.Cookies.Append("USER_DATA", jsonStr);
            }

            return RedirectToPage("/Index");
        }
    }
}

public class UserLogin
{
    [Required] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required] public bool RememberAccount { get; set; }
}

public enum Notilogin
{
    ChangePassword,
    Register,
    RoleUp
}