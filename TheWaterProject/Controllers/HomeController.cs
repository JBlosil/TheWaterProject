using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPACKAGE.LibDomain.Extensions;
using TheWaterProject.Models;
using TheWaterProject.Models.ViewModels;

namespace TheWaterProject.Controllers;

public class HomeController : Controller
{
    private IIntexRepository _repo;
    
    private readonly UserManager<IdentityUser> _userManager;
    public HomeController(IIntexRepository temp, UserManager<IdentityUser> userManager)
    {
        _repo = temp;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index() // Changed to async
    {
        int pageSize = 5;

        // Get currently logged-in user (assuming you have user login implemented)
        var currentUser = await _userManager.GetUserAsync(User);

        // Check if user is logged in
        if (currentUser == null || await _userManager.IsInRoleAsync(currentUser, "Admin"))
        {
            var blah = new ProductsListViewModel()
            {
                TopProducts = _repo.TopProducts
            };

            return View(blah);
        }

        // User is logged in, retrieve email
        var userEmail = currentUser.Email;

        // Find customer by email
        var customer = await _repo.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);

        int customerID = customer.CustomerId;

        var order = await _repo.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerID);

        int orderTransactionIdID = order.TransactionId;

        var lineitem = await _repo.LineItems.FirstOrDefaultAsync(li => li.TransactionId == orderTransactionIdID);

        int productID = lineitem.ProductId;

        var topUserProducts = await _repo.UserTopProducts.FirstOrDefaultAsync(utp => utp.product_ID == productID);

        List<int> recommendedProductIDs = new List<int>() { topUserProducts.Rec1, topUserProducts.Rec2, topUserProducts.Rec3, topUserProducts.Rec4, topUserProducts.Rec5 };

        var recommendedProducts = await _repo.Products.Where(p => recommendedProductIDs.Contains(p.product_ID)).ToListAsync();

        // Efficiently retrieve products based on recommended IDs
        
        var blah2 = new ProductsListViewModel()
        {
            UserEmail = userEmail,
            RecommendedProducts = recommendedProducts,
        };
        return View(blah2);
    }

    public IActionResult About()
    {
        return View();
    }
    
    [Authorize (Roles = "Admin")]
    public IActionResult ManageUsers()
    {
        IQueryable<IdentityUser> Users = _userManager.Users;
        
        return View(Users.ToList());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult EditUserPage()
    {
        return View(new IdentityUser());
    }
    
    [HttpGet]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> EditUser(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound();
        }
        
        return View("EditUserPage", user);
    }


    [HttpPost]
    public async Task<IActionResult> EditUser(IdentityUser user)
    {
        if (ModelState.IsValid)
        {
            // Assign the selected role (if applicable)
            if (!string.IsNullOrEmpty(user.SecurityStamp))
            {
                await _userManager.AddToRoleAsync(user, user.SecurityStamp);
            }

            return RedirectToAction("ManageUsers");
        }

        return View("ManageUsers");
    }


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
 [Authorize(Roles = "Admin")]
 public IActionResult ManageProducts(int? productID = null, int? numParts = null)
 {
     IQueryable<Product> products = _repo.Products; // Start with all products

     // Apply filters based on provided parameters
     if (productID.HasValue)
     {
         products = products.Where(p => p.product_ID == productID);
     }

     if (numParts.HasValue)
     {
         products = products.Where(p => p.Num_Parts == numParts);
     }

     return View("ManageProducts", products.ToList());
 }

    // GET: Admin/Products/Create
    [Authorize (Roles = "Admin")]
    public IActionResult CreateProduct()
    {
        return View(new Models.Product());
    }

    // POST: Admin/Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize (Roles = "Admin")]
    public IActionResult CreateProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            _repo.AddProduct(product);
            _repo.SaveChanges();
            return RedirectToAction("ManageProducts");
        }
        return View("CreateProduct", product);
    }
    
    [HttpGet]
    [Authorize (Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var recordEdit = _repo.Products
            .Single(x => x.product_ID == id);


        return View("CreateProduct", recordEdit);
    }

    // POST: Admin/Products/Edit/5
    [HttpPost]
    [Authorize (Roles = "Admin")]
    public IActionResult Edit(Models.Product updatedInfo)
    {
        _repo.UpdateProduct(updatedInfo);
        _repo.SaveChanges();

        return RedirectToAction("ManageProducts");
    }

    [HttpGet]
    public IActionResult DeleteProduct(int id)
    {
        var recordDelete = _repo.Products
            .Single(x => x.product_ID == id);

        return View(recordDelete);
    }
    
    [HttpPost]
    public IActionResult DeleteProduct(Models.Product producttodelete)
    {
        _repo.DeleteProduct(producttodelete.product_ID);
        _repo.SaveChanges();

        return RedirectToAction("ManageProducts");
    }
    
    [Authorize (Roles = "Customer")]
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
    
    [Authorize (Roles = "Admin")]
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
    
    public async Task<IActionResult> ProductDetails(int productId)
    {
        var product = _repo.Products.FirstOrDefault(p => p.product_ID == productId);
        
        if (product == null)
        {
            // Product not found, redirect to error or display an error message
            return NotFound(); // Use built-in NotFound() for HTTP 404 status code
            // Alternatively:
            // return View("ProductNotFound"); // Redirect to a specific error view
        }
        var itemRecommendations = await _repo.ItemRecommendations.FirstOrDefaultAsync(ir => ir.product_ID == productId);

        List<int> recommendedProductIDs = new List<int>() { itemRecommendations.Rec1, itemRecommendations.Rec2, itemRecommendations.Rec3, itemRecommendations.Rec4, itemRecommendations.Rec5 };

        var recommendedProducts = await _repo.Products.Where(p => recommendedProductIDs.Contains(p.product_ID)).ToListAsync();

        var details = new ProductDetailsViewModel()
        {
            Product = product,
            ItemRecommendedProducts = recommendedProducts
        };

        // if (product == null)
        // {
        //     return View("ProductNotFound"); // Create a view to handle not found case.
        // }

        return View(details);
    }

    [Authorize (Roles = "Customer")]
    public IActionResult Checkout()
    {
        return View();
    }

}