using MangaOnline.Enum;
using MangaOnline.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MangaOnline.Pages.Auth;

public class TestRoleUser : AbstractModel
{
    public IActionResult OnGet()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.Admin.ToString() }))return RedirectToPage("/Error");
        return Page();
    }
}