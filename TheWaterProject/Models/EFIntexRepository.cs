using MPACKAGE.LibManager.Models;

namespace TheWaterProject.Models;

public class EFIntexRepository : IIntexRepository
{
    private IntexDbContext _context;

    public EFIntexRepository(IntexDbContext temp)
    {
        _context = temp;
    }

    public IQueryable<Product> Products => _context.Products;
    public IQueryable<TopProducts> TopProducts => _context.TopProducts;
    public IQueryable<Order> Orders => _context.Orders;
    public IQueryable<Customer> Customers => _context.Customers;
    public IQueryable<LineItem> LineItems => _context.LineItems;
    public IQueryable<UserTopProducts> UserTopProducts => _context.UserTopProducts;
    public IQueryable<ItemRecommendations> ItemRecommendations => _context.ItemRecommendations;
    
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
    }
    
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
    }

    public void AddCustomer(Order order)
    {
        _context.Orders.Add(order);
    }
    
    public void DeleteProduct(int product_ID)
    {
        var productToDelete = _context.Products.FirstOrDefault(p => p.product_ID == product_ID);
        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
    }
}