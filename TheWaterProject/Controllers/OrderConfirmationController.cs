using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models.ViewModels;
using static Microsoft.AspNetCore.Mvc.Controller;

public class OrderConfirmationController : Controller
{
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
}