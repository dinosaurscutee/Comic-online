using MangaOnline.Enum;
using MangaOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace MangaOnline.Extensions;

public static class Job
{
    public static async void CheckUserVip()
    {
        try
        {
            using (var context = new MangaOnlineV1DevPRN221Context())
            {
                var roleNormalUserId = context.Roles.FirstOrDefault(x => x.Name!.Equals(UserRoleEnum.NormalUser.ToString()))!.Id;
                var roleUserVipId = context.Roles.FirstOrDefault(x => x.Name!.Equals(UserRoleEnum.VipUser.ToString()))!.Id;
                var listUserExpired = context.UserTokens
                    .Include(x => x.User)
                    .ThenInclude(x=>x.UserRole)
                    .Where(x => x.User.UserRole!.RoleId == roleUserVipId && x.Expires < DateTime.Now);
                foreach (var user in listUserExpired)
                {
                    user.Expires = DateTime.MaxValue;
                    user.User.UserRole!.RoleId = roleNormalUserId;
                }
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}