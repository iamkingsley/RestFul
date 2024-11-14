namespace DemoApp.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    public Task<int> SaveChangesAsync();
}