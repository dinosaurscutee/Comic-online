using MangaOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace MangaOnline.Pages.Shared.Components.Categories;

public class CategoriesViewComponent: ViewComponent
{
    private readonly MangaOnlineV1DevPRN221Context _context;

    public CategoriesViewComponent(MangaOnlineV1DevPRN221Context context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        List<Category> categories = _context.Categories.OrderBy(x => x.SubId).ToList();
        return View(categories);
    }
}