
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<int>>
{
    public async Task<Result<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new AddProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
        }
        // (TODO) Save product to data store
        return Result.Ok(1);
    }
}
