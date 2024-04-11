using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MPACKAGE.LibDomain.Extensions;

public class OrderReviewController : Controller
{
    private IntexDbContext _context;

    public OrderReviewController(IntexDbContext context)
    {
        _context = context;
    }

    public IActionResult OrderReview(int pageNum = 1, string? searchQuery = null, string? orderStatus = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        int pageSize = 50; // Number of items per page

        IQueryable<Order> ordersQuery = _context.Orders.Include(o => o.Customer);

        if (!string.IsNullOrEmpty(searchQuery) && int.TryParse(searchQuery, out int transactionId))
        {
            ordersQuery = ordersQuery.Where(o => o.TransactionId == transactionId);
        }    
        if (!string.IsNullOrEmpty(orderStatus))
        {
            ordersQuery = ordersQuery.Where(o => EF.Functions.Like(o.OrderStatus, orderStatus)); 
        }
        if (startDate.HasValue)
        {
            ordersQuery = ordersQuery.Where(o => o.Date >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            // Adding one day to include the end date in the results
            ordersQuery = ordersQuery.Where(o => o.Date <= endDate.Value.AddDays(1).AddTicks(-1));
        }

        int totalFilteredItems = ordersQuery.Count();
        
        var orders = ordersQuery
            .OrderByDescending(o => o.Date) 
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderReviewViewModel.OrderDetailsViewModel()
             {
                 TransactionId = o.TransactionId,
                 OrderStatus = o.OrderStatus,
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
                TotalItems = totalFilteredItems
            },
            SearchQuery = searchQuery, // Ensure these properties exist in your ViewModel
            OrderStatus = orderStatus,
            StartDate = startDate?.ToString("yyyy-MM-dd"),
            EndDate = endDate?.ToString("yyyy-MM-dd")
        };

        return View(viewModel);
    }
    // [HttpPost]
    // public async Task<IActionResult> PerformMassAction(string action, int[] selectedTransactions)
    // {
    //     // First, ensure we have orders to work with
    //     if (selectedTransactions == null || !selectedTransactions.Any())
    //     {
    //         // Optionally, add a message to TempData or ViewBag to inform no orders were selected
    //         return RedirectToAction("OrderReview");
    //     }
    //
    //     // Retrieve the orders from the database
    //     var ordersToUpdate = await _context.Orders
    //         .Where(o => selectedTransactions.Contains(o.TransactionId)) // Assuming TransactionId is your identifier
    //         .ToListAsync();
    //
    //     switch (action)
    //     {
    //         case "processing":
    //             foreach (var order in ordersToUpdate)
    //             {
    //                 order.OrderStatus = "Processing"; // Assuming OrderStatus is a string
    //             }
    //             break;
    //         case "onHold":
    //             foreach (var order in ordersToUpdate)
    //             {
    //                 order.OrderStatus = "On Hold for Fraud"; // Adjust the status as per your application's requirements
    //             }
    //             break;
    //         case "shipped":
    //             foreach (var order in ordersToUpdate)
    //             {
    //                 order.OrderStatus = "Shipped";
    //             }
    //             break;
    //         case "canceled":
    //             foreach (var order in ordersToUpdate)
    //             {
    //                 order.OrderStatus = "Canceled";
    //             }
    //             break;
    //         case "completed":
    //             foreach (var order in ordersToUpdate)
    //             {
    //                 order.OrderStatus = "Completed";
    //             }
    //             break;
    //     }
    //
    //     // Save changes if any updates were made
    //     if (ordersToUpdate.Any())
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //
    //     // Redirect back to the OrderReview page
    //     return RedirectToAction("OrderReview");
    // }

}