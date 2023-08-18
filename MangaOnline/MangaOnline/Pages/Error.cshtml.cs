using MangaOnline.Enum;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages;

public class ErrorModel : PageModel
{
    public string MessageError { get; set; }
    public ErrorTypeEnum errorType { get; set; }

    public void OnGet(ErrorTypeEnum errorType)
    {
        switch (errorType)
        {
            case ErrorTypeEnum.NoPage:
                MessageError = "Trang bạn đang tìm kiếm không có sẵn!";
                break;
            case ErrorTypeEnum.NoPermission:
                MessageError = "Role của bạn không phù hợp để truy cập trang này!";
                break;
        }
    }
}