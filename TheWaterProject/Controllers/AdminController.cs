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
} 
