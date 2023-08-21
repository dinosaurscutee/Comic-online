using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.ModelCore.ModelVnPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaOnline.Pages.Auth;

public class SubAccountVip : AbstractModel
{
    [BindProperty] public string PackageAccount { get; set; } = LogicManga.GetEnumDescription(PackageAccountVipEnum.OneWeek);
    private readonly IHttpContextAccessor _httpContextAccessor;
    [BindProperty] public string Ip { get; set; } = "sad";

    public SubAccountVip(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IActionResult OnGet()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.NormalUser.ToString(), UserRoleEnum.VipUser.ToString() }))
            return RedirectToPage("/Error");
        Ip = HttpContext.Connection.LocalIpAddress?.ToString()??"123";
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.NormalUser.ToString(), UserRoleEnum.VipUser.ToString() }))
            return RedirectToPage("/Error");
        try
        {
            var config = new ConfigurationBuilder().AddJsonFile("vnpayConfig.json").Build();

            var vnp_Returnurl = config["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            var vnp_Url = config["vnp_Url"];//URL thanh toan cua VNPAY 
            var vnp_TmnCode = config["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            var vnp_HashSecret = config["vnp_HashSecret"];
        
            //Get payment input
            var order = new OrderInfo
            {
                OrderId = DateTime.Now.Ticks, // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                Status = "0", //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
                CreatedDate = DateTime.Now
            };

            switch (PackageAccount)
            {
                case "1 tháng":
                    order.Amount = 100000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                    break;
                case "1 tuần":
                    order.Amount = 50000; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                    break;
            }
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", GetIpAddress()??"");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            // Response.Redirect(paymentUrl);
        }
        catch
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }
    
    public string? GetIpAddress()
    {
        string? ipAddress;
        try
        {
            ipAddress = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"];
            

            if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown")|| ipAddress.Length>45)
                ipAddress = _httpContextAccessor.HttpContext?.Request.Headers["REMOTE_ADDR"];
        }
        catch (Exception ex)
        {
            ipAddress = "Invalid IP:" + ex.Message;
        }

        return ipAddress;
    }
}