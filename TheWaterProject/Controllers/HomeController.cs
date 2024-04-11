using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class HomeController : Controller
{
    private IIntexRepository _repo;

    public HomeController(IIntexRepository temp)
    {
        _repo = temp;
    }
    public IActionResult Index(int? customer_ID)
    {
        int pageSize = 5;

        var blah2 = new ProductsListViewModel()
        {
            TopProducts = _repo.TopProducts,
        };
        return View(blah2);
    }
}