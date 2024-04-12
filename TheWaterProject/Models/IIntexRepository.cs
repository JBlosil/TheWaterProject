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

    // IQueryable<AspNetUser> AspNetUsers { get; }
    
    // // Does this need to be moved to IntexDbContext??
    // public IntexDbContext(IntexDbContextOptions<IntexDbContext> options)
    //     : base(options)
}