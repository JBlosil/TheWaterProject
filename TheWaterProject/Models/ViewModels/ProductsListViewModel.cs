namespace TheWaterProject.Models.ViewModels;

public class ProductsListViewModel
{
    public IQueryable<Product> Products { get; set; }
    public IQueryable<TopProducts> TopProducts { get; set; }
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    
    public string? CurrentProductCategory { get; set;}
    
}