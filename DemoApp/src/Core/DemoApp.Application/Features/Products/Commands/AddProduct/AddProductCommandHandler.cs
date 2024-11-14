
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
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

        var product = mapper.Map<Product>(request);
        await unitOfWork.Products.AddAsync(product, cancellationToken);
        var value = await unitOfWork.SaveChangesAsync();
        return value >= 1 ? Result.Ok(product.Id) : Result.Fail("Operation failed");
    }
}
