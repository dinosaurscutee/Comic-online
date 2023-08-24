using MangaOnline.Models;
using MangaOnline.Pages.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaOnline.Pages.Manage.User;

public class UserList : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;

    public List<Models.User> Users { get; set; }

    public string Role { get; set; }
    public string Status { get; set; }

    public int LastPage { get; set; }
    public int PageSize { get; set; } = 12;
    public int PageIndex { get; set; } = 1;

    public UserList(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }

    public void OnGet(string role, string status, int index)
    {
        Users = _context.Users.Include(x => x.UserRole)
            .ThenInclude(x => x!.Role)
            .Include(x => x.UserToken)
            .ToList();

        Role = string.IsNullOrEmpty(role) ? "Tất cả" : role;
        Status = string.IsNullOrEmpty(status) ? "Tất cả" : status;

        var roleStr = Role switch
        {
            "Admin" => "Admin",
            "Vip User" => "VipUser",
            "Normal User" => "NormalUser",
            _ => "All"
        };

        var statusEnum = Status switch
        {
            "Bình thường" => (int)StatusUser.Normal,
            "Khóa" => (int)StatusUser.Lock,
            _ => -1
        };

        if (statusEnum != -1)
        {
            Users = Users.Where(x => x.Status == statusEnum).ToList();
        }

        if (!Role.Equals("Tất cả"))
        {
            Users = Users.Where(x => x.UserRole.Role.Name == roleStr).ToList();
        }

        LastPage = Users.Count / PageSize;
        if (Users.Count % PageSize > 0)
        {
            LastPage += 1;
        }

        index = index == 0 ? 1 : index;
        PageIndex = index;
        if (index <= LastPage)
        {
            Users = Users.Skip(PageSize * (index - 1)).Take(PageSize).ToList();
        }
    }

    public IActionResult OnPostChangeStatus(string userId, string userStatus)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id.ToString().Equals(userId));

        if (userStatus.Equals("0"))
        {
            user.Status = 1;
            _context.SaveChanges();
        }
        else
        {
            user.Status = 0;
            _context.SaveChanges();
        }

        return RedirectToPage("/Manage/User/UserList");
    }
}