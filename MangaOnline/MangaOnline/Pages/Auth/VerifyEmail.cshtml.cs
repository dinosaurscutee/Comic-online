using MangaOnline.Enum;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Auth;

public class VerifyEmail : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;
    public VerifyEmail( MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }
    public string Message { get; set; } 
    
    public IActionResult OnGet(Guid id)
    {
        var User =  _context
            .Users
            .FirstOrDefault(x => x.Id == id && x.EmailConfirmed == false);
        if (User is null)
        {
            return RedirectToPage("/Error",new {type=ErrorTypeEnum.NoPage});
        }

        Message = "Xác nhận email thành công";
        return Page();
    }
}