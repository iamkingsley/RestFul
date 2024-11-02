
namespace DemoApp.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<Result<IEnumerable<ProductResponse>>>;
