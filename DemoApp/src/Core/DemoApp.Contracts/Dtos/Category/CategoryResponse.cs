
using DemoApp.Contracts.Dtos.Products;

namespace DemoApp.Contracts.Dtos.Category;

public record CategoryResponse : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ProductResponse> Products { get; set; }
}
