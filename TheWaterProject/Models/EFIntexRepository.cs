namespace TheWaterProject.Models;

public class EFIntexRepository : IIntexRepository
{
    private IntexDbContext _context;

    public EFIntexRepository(IntexDbContext temp)
    {
        _context = temp;
    }

    public IQueryable<Product> Products => _context.Products;
    public IQueryable<Order> Orders => _context.Orders;

    
    // IQueryable<AspNetUser> AspNetUsers { get; }
} 