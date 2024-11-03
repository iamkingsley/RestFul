
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<int>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
        _productRepository.AddProduct(_mapper.Map<Product>(request));
        return Result.Ok(1);
    }
}
