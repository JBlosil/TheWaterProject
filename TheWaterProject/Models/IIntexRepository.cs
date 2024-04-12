using MPACKAGE.LibManager.Models;

namespace TheWaterProject.Models;

public interface IIntexRepository
{
    public IQueryable<Product> Products { get; }
    public IQueryable<TopProducts> TopProducts { get; }
    public IQueryable<Order> Orders { get; }
    public IQueryable<LineItem> LineItems { get; }
    
    public IQueryable<Customer> Customers { get; }
    
    public IQueryable<UserTopProducts> UserTopProducts { get; }
    
    public IQueryable<ItemRecommendations> ItemRecommendations { get; }

    void DeleteProduct(int product_ID);
    void UpdateProduct(Product product);
    void AddProduct(Product product);
    void SaveChanges();
}