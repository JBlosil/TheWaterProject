namespace TheWaterProject.Models.ViewModels;

public class ProductsListViewModel
{
    public IQueryable<Product> Products { get; set; }
    public IQueryable<TopProducts> TopProducts { get; set; }
    public IQueryable<Customer> Customers { get; set; }
    public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    public IQueryable<Order> Orders { get; set; }
    public IQueryable<LineItem> LineItems { get; set; }
    public string? CurrentProductCategory { get; set;}
    public string? UserEmail { get; set; }
    public int? CustomerId { get; set; }
    public List<Product> RecommendedProducts { get; set; }
}