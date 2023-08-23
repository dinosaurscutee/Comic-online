using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Auth;

public class AuthLogout : PageModel
{
    public IActionResult OnGet()
    {
        Response.Cookies.Delete("ACCESS_TOKEN");
        Response.Cookies.Delete("USER_DATA");
        Response.Cookies.Delete("MANGA_FOLLOW");
        return RedirectToPage("/Auth/AuthLogin");
    }
}