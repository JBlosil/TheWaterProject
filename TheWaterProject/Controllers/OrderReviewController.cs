using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            .Include(o => o.Customer)
            .OrderByDescending(o => o.Date) // Assuming 'Date' is the date of the transaction
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize);
            
         var orders = ordersQuery  
             .Select(o => new OrderReviewViewModel.OrderDetailsViewModel()
             {
                 TransactionId = o.TransactionId,
                 Amount = o.Amount,
                 Date = o.Date,
                 TransactionCountry = o.CountryOfTransaction,
                 ShippingAddress = o.ShippingAddress,
                 Fraud = o.Fraud,
                 CustomerName = o.Customer.FirstName + " " + o.Customer.LastName 
             })
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