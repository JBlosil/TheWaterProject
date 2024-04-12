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
}