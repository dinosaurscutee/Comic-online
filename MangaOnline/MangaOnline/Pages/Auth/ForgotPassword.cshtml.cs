using MangaOnline.Extensions;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Auth;

public class ForgotPassword : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;

    private readonly ILogicHandler _logicHandler;

    public ForgotPassword(MangaOnlineV1DevPRN221Context context, ILogicHandler logicHandler)
    {
        _context = context;
        _logicHandler = logicHandler;
    }

    [BindProperty] public string Email { get; set; }

    public string Noti { get; set; }

    public IActionResult OnPost()
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);

        if (user is null)
        {
            Noti = "Email không tồn tại";
            return Page();
        }

        var tempPass = _logicHandler.GeneratePassword(12);

        _logicHandler.SendEmailAsync(Email, "[MangaOnline] Reset Password",
            $"Your new password is: {tempPass}.");

        user.Password = BCrypt.Net.BCrypt.HashPassword(tempPass);
        _context.SaveChanges();

        return RedirectToPage("/Auth/AuthLogin");
    }
}