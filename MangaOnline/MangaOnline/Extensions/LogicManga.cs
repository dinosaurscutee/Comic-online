using System.ComponentModel;
using System.Reflection;

namespace MangaOnline.Extensions;

public static class LogicManga
{
    public static string GetEnumDescription(System.Enum value)
    {
        var fileInfo = value.GetType().GetField(value.ToString())!;

        if (fileInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }
}