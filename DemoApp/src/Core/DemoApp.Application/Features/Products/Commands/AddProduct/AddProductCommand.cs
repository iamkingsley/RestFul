
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public record AddProductCommand(
    string ProductName,
    string Description, 
    int CategoryId, 
    double Price
) : IRequest<Result<int>>;
