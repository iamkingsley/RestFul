

using DemoApp.Domain.Entities;

namespace DemoApp.Persistence.Data;

public static class InMemoryData
{
    public static List<Product> Products = new() { };
    public static List<Category> Categories = new() { };
}
