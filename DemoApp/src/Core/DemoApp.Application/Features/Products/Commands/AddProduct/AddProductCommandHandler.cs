
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<AddProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
        await productRepository.AddAsync(mapper.Map<Product>(request), cancellationToken);
        return Result.Ok(new Guid());
    }
}
