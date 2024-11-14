
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public record AddProductCommand(
    string ProductName,
    string Description, 
    Guid CategoryId, 
    double Price
) : IRequest<Result<Guid>>;
