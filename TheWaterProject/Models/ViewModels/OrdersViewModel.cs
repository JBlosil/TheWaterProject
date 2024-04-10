namespace TheWaterProject.Models.ViewModels;

public class OrdersViewModel
{
    public IQueryable<Order> Orders { get; set; }
    public int? Fraud { get; set;}

    
}