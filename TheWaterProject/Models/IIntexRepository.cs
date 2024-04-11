namespace TheWaterProject.Models;

public interface IIntexRepository
{
    IQueryable<Product> Products { get; }
    IQueryable<Order> Orders { get; }
    
    // IQueryable<AspNetUser> AspNetUsers { get; }
    
    // // Does this need to be moved to IntexDbContext??
    // public IntexDbContext(IntexDbContextOptions<IntexDbContext> options)
    //     : base(options)
}   