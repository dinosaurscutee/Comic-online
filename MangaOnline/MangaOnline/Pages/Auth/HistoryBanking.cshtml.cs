using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.ModelCore;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Auth;

public class HistoryBanking : AbstractModel
{
    public List<History> list { get; set; }
    public bool StatusSub { get; set; }

    private readonly MangaOnlineV1DevPRN221Context _context;

    public HistoryBanking(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }

    public IActionResult OnGetAsync(string? vnp_TxnRef, int? vnp_Amount, bool statusSub)
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.NormalUser.ToString(), UserRoleEnum.VipUser.ToString() }))
            return RedirectToPage("/Error");

        StatusSub = statusSub;
        var accessToken = HttpContext.Request.Cookies["USER_DATA"];
        var user = JsonConvert.DeserializeObject<UserCookie>(accessToken!)!;
        var userRole = _context.UserRoles.Include(x => x.User)
            .ThenInclude(x => x.UserToken).FirstOrDefault(x => x.UserId == user.Id);
        var role = _context.Roles.FirstOrDefault(x => x.Name!.Equals(UserRoleEnum.VipUser.ToString()));
        var statusPackage = _context.Histories.FirstOrDefault(x =>
            x.Value == vnp_TxnRef && x.Hash == ((int)PackageAccountVipEnum.NonePackage).ToString());
        if (vnp_TxnRef != null &&
            vnp_Amount != null &&
            statusPackage != null)
        {
            userRole!.RoleId = role!.Id;
            statusPackage.From = DateTime.Now.ToString("dd/MM/yyyy"); // bđ gói
            statusPackage.Date = DateTime.Now; // ngày tạo

            switch (vnp_Amount)
            {
                case 10000000:
                    statusPackage.Hash = ((int)PackageAccountVipEnum.OneMonth).ToString();
                    userRole.User.UserToken!.Expires = DateTime.Now.AddDays(30);
                    break;
                case 5000000:
                    statusPackage.Hash = ((int)PackageAccountVipEnum.OneWeek).ToString();
                    userRole.User.UserToken!.Expires = DateTime.Now.AddDays(7);
                    break;
            }
            statusPackage.To = userRole.User.UserToken!.Expires.ToString("dd/MM/yyyy"); // bđ gói
            _context.SaveChanges();
            var token = AuthenticationPage.WriteToken(user.FullName, user.Email, userRole.Role.Name!);
            var userCookie = new UserCookie()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = userRole.Role.Name!,
                PhoneNumber = user.PhoneNumber ?? "",
                Avartar = userRole.User.Avatar
            };
            var jsonStr = JsonConvert.SerializeObject(userCookie);
            Response.Cookies.Append("ACCESS_TOKEN", token);
            Response.Cookies.Append("USER_DATA", jsonStr);
            StatusSub = true;
            return Redirect("/sub-account-vip/pay-result?statusSub=true");
        }
        else
        {
            list = _context.Histories.Where(r =>
                r.User == user.Id.ToString() && r.Hash != ((int)PackageAccountVipEnum.NonePackage).ToString()).ToList();
            return Page();
        }
    }
}