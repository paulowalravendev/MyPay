using FluentValidation;

namespace MyPay.Application.Features.Customers.LoginCustomer;

internal sealed class LoginCustomerCommandValidator : AbstractValidator<LoginCustomerCommand>
{
    public LoginCustomerCommandValidator()
    {
        RuleFor(c => c.Cpf).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
