﻿using MangaOnline.Enum;
using MangaOnline.Extensions;
using MangaOnline.ModelCore;
using MangaOnline.ModelCore.ModelVnPay;
using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MangaOnline.Pages.Auth;

public class SubAccountVip : AbstractModel
{
    private readonly MangaOnlineV1DevPRN221Context _context;
    [BindProperty] public string PackageAccount { get; set; } = LogicManga.GetEnumDescription(PackageAccountVipEnum.OneWeek);
    private readonly IHttpContextAccessor _httpContextAccessor;
    [BindProperty] public string Ip { get; set; } = "sad";

    public SubAccountVip(IHttpContextAccessor httpContextAccessor, MangaOnlineV1DevPRN221Context context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
    
    public IActionResult OnGet()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.NormalUser.ToString() }))
            return RedirectToPage("/Error");
        Ip = HttpContext.Connection.LocalIpAddress?.ToString()??"123";
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!CheckRoleUser(new[] { UserRoleEnum.NormalUser.ToString() }))
            return RedirectToPage("/Error");
        try
        {
            var accessToken = HttpContext.Request.Cookies["USER_DATA"];
            var user = JsonConvert.DeserializeObject<UserCookie>(accessToken!)!;
            
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
            vnpay.AddRequestData("vnp_IpAddr", GetIpAddress()??"123456789");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            
            var tran = new History()
            {
                Id = Guid.NewGuid(),
                User = user.Id.ToString().ToUpper(),
                From = DateTime.Now.ToString("dd/MM/yyyy"), // bđ gói
                To = DateTime.Now.ToString("dd/MM/yyyy"), // bđ gói
                Value = order.OrderId.ToString(),
                Date = DateTime.Now, // ngày tạo,
                Hash = ((int)PackageAccountVipEnum.NonePackage).ToString()
            };

            var listHistoriesOld =
                _context.Histories.Where(x => x.Hash == ((int)PackageAccountVipEnum.NonePackage).ToString() && x.User == tran.User);
            if (listHistoriesOld.Any())
            {
                _context.RemoveRange(listHistoriesOld);
            }
            _context.Histories.Add(tran);
            _context.SaveChangesAsync();
            return Redirect(paymentUrl);
        }
        catch
        {
            return RedirectToPage("/Error");
        }
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