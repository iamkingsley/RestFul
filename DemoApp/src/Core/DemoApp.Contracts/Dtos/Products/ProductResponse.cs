
namespace DemoApp.Contracts.Dtos.Products;
public record ProductResponse : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryResponse Category { get; set; }
    public int CategoryId { get; set; }
    public double Price { get; set; }
}

