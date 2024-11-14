using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Persistence.Data;

namespace DemoApp.Persistence.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IProductRepository _productRepository;
    private ICategoryRepository _categoryRepository;

    public IProductRepository Products => _productRepository ?? new ProductRepository(context);
    public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(context); 

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}