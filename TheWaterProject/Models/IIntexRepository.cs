namespace TheWaterProject.Models;

public interface IIntexRepository
{
    public IQueryable<Product> Products { get; }
    
    public IQueryable<TopProducts> TopProducts { get; }
}