using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class HomeController : Controller
{
    private IIntexRepository _repo;

    public HomeController(IIntexRepository temp)
    {
        _repo = temp;
    }
    public IActionResult Index(int? customer_ID)
    {
        int pageSize = 5;

        var blah2 = new ProductsListViewModel()
        {
            TopProducts = _repo.TopProducts,
        };
        return View(blah2);
    }
    
        // public IActionResult ManageUsers()
    // {
    //     var users = _repo.AspNetUser.ToList();
    //     return View(users);
    // }


    // [HttpPost]
    // public IActionResult CreateUser(UserViewModel viewModel)
    // {
            // var viewModel = new UserViewModel
    //     if (ModelState.IsValid)
    //     {
             // // Save viewModel.Customer to the database
             // // You might need to handle the role assignment separately, depending on your database design
                // _repo.Customers.Add(viewModel.AspNetUser);
                // _repo.SaveChanges();
                // return RedirectToAction(nameof(Index));
    //     }
    
            // // Reload the roles in case of an error
            // viewModel.Roles = _repo.Roles.Select(r => new SelectListItem 
            // { 
            //     ID = r.RoleId.ToString(),
            //     Name = r.RoleName
            // }).ToList();
            
    //     return View(viewModel);
    // }

    // public IActionResult Edit(int id)
    // {
    //     var user = _repo.Customers.Find(Id);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(user);
    // }

//     [HttpPost]
//     public IActionResult Edit(AspNetUser user)
//     {
//         if (ModelState.IsValid)
//         {
//             _repo.Update(user);
//             _repo.SaveChanges();
//             return RedirectToAction(nameof(Index));
//         }
//         return View(user);
//     }
//
//     public IActionResult Delete(int id)
//     {
//         var customer = _repo.AspNetUsers.Find(id);
//         if (customer != null)
//         {
//             _repo.Customers.Remove(customer);
//             _repo.SaveChanges();
//         }
//         return RedirectToAction(nameof(Index));
//     }
    



//
// THIS PART IS FOR THE PRODUCTS STUFF
// A LOT IS COMMENTED OUT BECAUSE IT DOESN'T RECOGNIZE THINGS LIKE .Find, .Add, or .SaveChanges
// I FORGET HOW TO DO THIS PART... IT'S SOMETHING WITH THE DB CONTEXT BUT IDK
//

 // GET: Admin/Products
    public IActionResult ManageProducts()
    {
        var products = _repo.Products.ToList();
        return View("ManageProducts", products);
    }

    // GET: Admin/Products/Create
    public IActionResult CreateProducts()
    {
        return View("CreateProduct");
    }

    // POST: Admin/Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            // _repo.Products.Add(product);
            // _repo.SaveChanges();
            return RedirectToAction("ManageProducts");
        }
        return View("CreateProduct", product);
    }

    // // GET: Admin/Products/Edit/5
    // public IActionResult EditProduct(int id)
    // {
    //     var product = _repo.Products.Find(id);
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //     return View("ManageProducts", product);
    // }

    // POST: Admin/Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditProduct(int id, Product product)
    {
        if (id != product.product_ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // _repo.Update(product);
            // _repo.SaveChanges();
            return RedirectToAction("ManageProducts");
        }
        return View("ManageProducts");
    }

    // // GET: Admin/Products/Delete/5
    // public IActionResult DeleteProduct(int id)
    // {
    //     var product = _repo.Products.Find(id);
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //     return View("Products/Delete", product);
    // }

    // // POST: Admin/Products/Delete/5
    // [HttpPost, ActionName("DeleteProduct")]
    // [ValidateAntiForgeryToken]
    // public IActionResult DeleteConfirmed(int id)
    // {
    //     var product = _repo.Products.Find(id);
    //     _repo.Products.Remove(product);
    //     _repo.SaveChanges();
    //     return RedirectToAction("ManageProducts");
    // }
    
    public IActionResult OrderConfirmation(string orderNumber)
    {

// I DON'T THINK I NEED THIS PART....
// // Fetch order details from database or service
        var blah = new OrdersViewModel()
        {
            // CustomerName = "John Doe", // This would be fetched from the database
            // OrderNumber = orderNumber,
            // Email = "john.doe@example.com", // This would be fetched from the database
            // IsSuspectedFraud = CheckForFraud(orderNumber) // Some method to determine fraud
        };


// Send the model to the view
        return View();
    }


    // private bool CheckForFraud(string orderNumber)
    // {
    //     // Fill in logic here
    //     
    // };
    
    public IActionResult OrderReview(int pageNum = 1, string? searchQuery = null, string? orderStatus = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        int pageSize = 50; // Number of items per page

        IQueryable<Order> ordersQuery = _repo.Orders.Include(o => o.Customer);

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
    //     var ordersToUpdate = await _reop.Orders
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
    //         await _reop.SaveChangesAsync();
    //     }
    //
    //     // Redirect back to the OrderReview page
    //     return RedirectToAction("OrderReview");
    // }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult Products(int pageNum, string? productCategory)
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