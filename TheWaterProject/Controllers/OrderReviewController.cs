using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;
using System.Linq;

public class OrderReviewController : Controller
{
    private IntexDbContext _context;

    public OrderReviewController(IntexDbContext context)
    {
        _context = context;
    }

    public IActionResult OrderReview(int pageNum = 1)
    {
        int pageSize = 50; // Number of items per page
        var ordersQuery = _context.Orders
            .OrderByDescending(o => o.Date) // Assuming 'Date' is the date of the transaction
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize);
            
         var orders = ordersQuery  
             .Select(o => new OrderReviewViewModel()
                 {
                     TransactionId = o.TransactionId
                     Amount = o.
                 } 
             )
            .ToList();
        
        var viewModel = new OrderReviewViewModel
        {
            Orders = orders,
            PaginationInfo = new PaginationInfo
            {
                CurrentPage = pageNum,
                ItemsPerPage = pageSize,
                TotalItems = _context.Orders.Count()
            }
        };

        return View(viewModel);
    }
}