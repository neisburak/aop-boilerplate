using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(r => r.ProductName).NotEmpty().WithMessage(ValidationMessages.ProductNameEmpty);
        RuleFor(r => r.ProductName).Length(2, 30);
        RuleFor(r => r.ProductName).Must(StartsWithA);

        RuleFor(r => r.UnitPrice).NotEmpty();
        RuleFor(r => r.UnitPrice).GreaterThanOrEqualTo(1);
        RuleFor(r => r.UnitPrice).GreaterThanOrEqualTo(10).When(w => w.CategoryId == 10);
    }

    private bool StartsWithA(string value)
    {
        return value.ToLower().StartsWith('a');
    }
}