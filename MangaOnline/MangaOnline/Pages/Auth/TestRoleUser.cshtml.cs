using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.Pages.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MangaOnline.Pages.Auth;

public class TestRoleUser : AbstractModel
{
    private IHubContext<NotificationHub> HubContext;

    public TestRoleUser(IHubContext<NotificationHub> hubContext)
    {
        HubContext = hubContext;
    }
    public async Task<IActionResult> OnGet()
    {
        await HubContext.Clients.All.SendAsync("LoadNotification", "a2c399d9-3465-4594-a56b-6785cb4814fb");
        if (!CheckRoleUser(new[] { UserRoleEnum.Admin.ToString() }))return RedirectToPage("/Error");
        return Page();
    }
}