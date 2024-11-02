
namespace DemoApp.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(p => p.ProductName).NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(p => p.Price).GreaterThan(0)
            .NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(p => p.CategoryId).NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required");
    }
}
