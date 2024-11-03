using DemoApp.Domain.Entities;

namespace DemoApp.Application.Interfaces.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAllAsync();
    Product? GetAsync(int id);
    void DeleteAsync(int id);
    int AddProduct(Product product);
}
