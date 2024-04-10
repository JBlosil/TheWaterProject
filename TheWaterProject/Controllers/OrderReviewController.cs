using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TheWaterProject.Controllers;

public class OrderReviewController : Controller
{
    public IActionResult OrderReview()
    {
        return View();
    }
}