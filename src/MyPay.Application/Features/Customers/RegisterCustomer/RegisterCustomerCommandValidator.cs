using FluentValidation;

namespace MyPay.Application.Features.Customers.RegisterCustomer;

internal sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(c => c.Fullname).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Cpf).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
