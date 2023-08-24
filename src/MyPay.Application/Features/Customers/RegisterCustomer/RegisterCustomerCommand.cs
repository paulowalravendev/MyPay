using MyPay.Application.Abstractions.Messaging;

namespace MyPay.Application.Features.Customers.RegisterCustomer;

public sealed record RegisterCustomerCommand(
    string Fullname,
    string Email,
    string Cpf,
    string Password) : ICommand<Guid>;
