using System.ComponentModel;

namespace MangaOnline.Enum;

public enum PackageAccountVipEnum
{
    [Description("1 tuần")] 
    OneWeek = 1,
    [Description("1 tháng")] 
    OneMonth = 2,
    [Description("Chưa thanh toán gói")] 
    NonePackage = 3
}