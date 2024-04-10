namespace TheWaterProject.Models.ViewModels;

public class ProductDetailsViewModel
{
    public IQueryable<Product> Products { get; set; }
    
    public string? ProductName { get; set; }
}