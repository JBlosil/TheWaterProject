using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class PrivacyPolicy : Controller{

    public IActionResult Privacy()
    {
        return View();
    }
}