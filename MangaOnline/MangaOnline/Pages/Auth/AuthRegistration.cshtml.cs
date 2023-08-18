using System.ComponentModel.DataAnnotations;
using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MangaOnline.Pages.Auth;

public class AuthRegistration : PageModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;
    private readonly ILogicHandler _logicHandler;

    public AuthRegistration(MangaOnlineV1DevPRN221Context mangaOnlineV1DevContext, ILogicHandler logicHandler)
    {
        _context = mangaOnlineV1DevContext;
        _logicHandler = logicHandler;
    }

    [BindProperty] public RequestRegister RequestRegister { get; set; }
    public string MessageEmail { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }


    public async Task<IActionResult> OnPost()
    {
        var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == RequestRegister.Email);
        var userId = Guid.NewGuid();
        if (checkUser is not null)
        {
            MessageEmail = "email đã được đăng ký";
            return Page();
        }
        else
        {
            var config = new ConfigurationBuilder().AddJsonFile("templateEmail.json").Build();
            var sendEmail = _logicHandler.SendEmailAsync(
                RequestRegister.Email,
                "[MangaOnline] Xác thực email",
                config["templateEmail:head"]
                + "http://localhost:5000//Auth/VerifyEmail?id="
                + userId
                + config["templateEmail:last"]);
            if (!await sendEmail)
            {
                MessageEmail = "đã xảy ra lỗi! kiểm tra lại email";
                return Page();
            }
        }

        var createUser = new User()
        {
            Id = userId,
            SubId = 0,
            FullName = RequestRegister.FullName,
            Email = RequestRegister.Email,
            EmailConfirmed = false,
            Password = BCrypt.Net.BCrypt.HashPassword(RequestRegister.Password),
            PhoneNumber = RequestRegister.PhoneNumber,
            PhoneNumberConfirmed = true,
            AccessFailedCount = 0,
            CreatedAt = DateTimeOffset.Now,
            ModifiedAt = DateTimeOffset.Now,
            IsActive = true,
            Status = (int)StatusUser.Normal
        };

        var roleNormalUserId =
            _context.Roles.FirstOrDefault(x => x.Name.Equals(UserRoleEnum.NormalUser.ToString()))!.Id;
        var createUserRoles = new UserRole
        {
            SubId = 0,
            UserId = createUser.Id,
            RoleId = roleNormalUserId
        };

        // Tạo mã JWT
        var token = AuthenticationPage.WriteToken(RequestRegister.FullName, RequestRegister.Email,
            UserRoleEnum.NormalUser.ToString());
        var userToken = new UserToken
        {
            Id = Guid.NewGuid(),
            UserId = createUser.Id,
            Email = createUser.Email,
            Expires = DateTime.UtcNow.AddDays(30),
            Value = token
        };

        _context.Users.Add(createUser);
        _context.UserRoles.Add(createUserRoles);
        _context.UserTokens.Add(userToken);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Auth/AuthLogin", new { notilogin = Notilogin.Register });
    }
}

public class RequestRegister
{
    [Required] public string FullName { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public enum StatusUser
{
    //bình thường
    Normal,

    // bị khóa
    Lock,
}