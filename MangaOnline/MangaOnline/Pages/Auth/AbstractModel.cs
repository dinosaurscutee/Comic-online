using MangaOnline.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Auth;

public class AbstractModel : PageModel
{
    public bool CheckRoleUser(string[] listRole)
    {
        try
        {
            var accessToken = Request.Cookies["ACCESS_TOKEN"];
            if (accessToken == null) return false;
            var user = AuthenticationPage.ReadToken(accessToken);
            if (user == null) return false;
            var userRole = user.role;
            return listRole.FirstOrDefault(x=>x==userRole)!=null;
        }
        catch
        {
            return false;
        }
    }
}