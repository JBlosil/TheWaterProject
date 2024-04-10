using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class ProductDetailsController : Controller
{
    private IIntexRepository _repo;

    public ProductDetailsController(IIntexRepository temp)
    {
        _repo = temp;
    }

    public IActionResult ProductDetails(int productId)
    {
        var product = _repo.Products.FirstOrDefault(p => p.product_ID == productId);

        var details = new ProductDetailsViewModel
        {
            Product = product
        };

        // if (product == null)
        // {
        //     return View("ProductNotFound"); // Create a view to handle not found case.
        // }

        return View(details);
    }
}