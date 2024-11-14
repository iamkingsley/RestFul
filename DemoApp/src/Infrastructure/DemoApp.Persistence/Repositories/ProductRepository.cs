
using System.Linq.Expressions;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Domain.Entities;
using DemoApp.Persistence.Data;

namespace DemoApp.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
}
