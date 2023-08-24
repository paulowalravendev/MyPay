using FluentValidation;

namespace MyPay.Application.Features.Shopkeepers.LoginShopkeeper;

internal sealed class LoginShopkeeperCommandValidator : AbstractValidator<LoginShopkeeperCommand>
{
    public LoginShopkeeperCommandValidator()
    {
        RuleFor(c => c.Cnpj).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
