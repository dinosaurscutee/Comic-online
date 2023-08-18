using System.ComponentModel.DataAnnotations;
using MangaOnline.Enum;
using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Auth;

public class ChangePassword : AbstractModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;
    public ChangePassword(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }
    [BindProperty] public RequiredChangePassword RequiredChangePassword { get; set; }
    public string MessageOldPassword { get; set; }
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.Admin.ToString(), UserRoleEnum.NormalUser.ToString(), UserRoleEnum.VipUser.ToString() }))
            return RedirectToPage("/Error");
        var accessToken = Request.Cookies["USER_DATA"];
        UserCookie userCookie = new UserCookie();
        userCookie = JsonConvert.DeserializeObject<UserCookie>(accessToken);
        if (userCookie is not null)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(userCookie.Email));
            if (!BCrypt.Net.BCrypt.Verify(RequiredChangePassword.OldPassword, user.Password))
            {
                MessageOldPassword = "mật khẩu sai";
                return Page();
            }
            else
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(RequiredChangePassword.NewPassword);
                await _context.SaveChangesAsync();
                Response.Cookies.Delete("ACCESS_TOKEN");
                Response.Cookies.Delete("USER_DATA");
                return RedirectToPage("/Auth/AuthLogin", new { notilogin = Notilogin.ChangePassword });
            }
        }
        return Page();
    }
}
public class RequiredChangePassword
{
    [Required]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}