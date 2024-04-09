using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TheWaterProject.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}