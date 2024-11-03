
using DemoApp.Application.Interfaces.Repositories;

namespace DemoApp.Application.Features.Products.Queries.GetAllProducts;
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductResponse>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<IEnumerable<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _productRepository.GetAllAsync();
        if (products == null)
        {
            return Result.Fail(new Error("Error occured while fetching data"));
        }
        var result = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return Result.Ok(result);
    }
}
