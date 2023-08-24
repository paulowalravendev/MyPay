using FluentValidation;

namespace MyPay.Application.Features.Shopkeepers.RegisterShopkeeper;

internal sealed class RegisterShopkeeperCommandValidator : AbstractValidator<RegisterShopkeeperCommand>
{
    public RegisterShopkeeperCommandValidator()
    {
        RuleFor(c => c.Fullname).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Cnpj).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
