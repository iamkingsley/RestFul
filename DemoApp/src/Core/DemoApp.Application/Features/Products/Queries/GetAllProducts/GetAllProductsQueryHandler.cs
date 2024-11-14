
namespace DemoApp.Application.Features.Products.Queries.GetAllProducts;
public class GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductResponse>>>
{
    public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetAllAsync();
        var result = mapper.Map<IEnumerable<ProductResponse>>(products);
        return Result.Ok(result);
    }
}
