namespace TheWaterProject.Models;

public interface IWaterRepository
{
    public IQueryable<Project> Projects { get; }
}