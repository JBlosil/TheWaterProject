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

    public IActionResult Index(int product_ID, string productName)
    {
        var details = new ProductDetailsViewModel()
        {
            Products = _repo.Products.Where(p => p.product_ID == product_ID),
            
            ProductName = productName
        };

        // if (product == null)
        // {
        //     return View("ProductNotFound"); // Create a view to handle not found case.
        // }

        return View(details);
    }
}