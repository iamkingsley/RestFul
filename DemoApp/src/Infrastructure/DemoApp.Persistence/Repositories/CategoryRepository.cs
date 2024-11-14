using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Domain.Entities;
using DemoApp.Persistence.Data;

namespace DemoApp.Persistence.Repositories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
}