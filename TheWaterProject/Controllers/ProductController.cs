using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class ProductController : Controller
{
    private IIntexRepository _repo;

    public ProductController(IIntexRepository temp)
    {
        _repo = temp;
    }
    public IActionResult Index(int pageNum, string? productCategory)
    {
        int pageSize = 5;

        var products = new ProductsListViewModel()
        {
            Products = _repo.Products
                .Where(x => x.Category==productCategory || productCategory == null)
                .OrderBy(x => x.Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = productCategory == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Category == productCategory).Count(),
            },
            
            CurrentProductCategory = productCategory
        };
        
        return View(products);
    }
}