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
}