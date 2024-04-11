using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;

namespace TheWaterProject.Controllers;

public class AdminController : Controller
{
    private readonly IIntexRepository _repo;

    public AdminController(IIntexRepository temp)
    {
        _repo = temp;
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

} 
