using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class OrderReviewController : Controller
{
    private IntexDbContext _context;

    public OrderReviewController(IntexDbContext context)
    {
        _context = context;
    }

    public IActionResult OrderReview(int pageNum = 1, string searchQuery = null)
    {
        int pageSize = 50; // Number of items per page

        IQueryable<Order> ordersQuery = _context.Orders.Include(o => o.Customer);

        if (!string.IsNullOrEmpty(searchQuery) && int.TryParse(searchQuery, out int transactionId))
        {
            ordersQuery = ordersQuery.Where(o => o.TransactionId == transactionId);
        }    
        
        var orders = ordersQuery
            .OrderByDescending(o => o.Date) // Assuming 'Date' is the date of the transaction
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
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