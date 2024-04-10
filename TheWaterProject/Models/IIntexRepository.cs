namespace TheWaterProject.Models;

public interface IIntexRepository
{
    IQueryable<Product> Products { get; }
    IQueryable<Order> Orders { get; }
}