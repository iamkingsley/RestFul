
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Domain.Entities;
using DemoApp.Persistence.Data;

namespace DemoApp.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    public int AddProduct(Product product)
    {
        product.Id = InMemoryData.Products.Count + 1;
        InMemoryData.Products.Add(product);
        return product.Id;
    }

    public void DeleteAsync(int id)
    {
        var product = InMemoryData.Products.Find(p => p.Id == id);
        if (product is not null)
        {
            InMemoryData.Products.Remove(product);
        }
    }

    public IEnumerable<Product> GetAllAsync()
    {
        return InMemoryData.Products;
    }

    public Product? GetAsync(int id)
    {
        var product = InMemoryData.Products.Find(p => p.Id == id);
        if (product is not null)
        {
            return product;
        }
        return null;
    }
}
