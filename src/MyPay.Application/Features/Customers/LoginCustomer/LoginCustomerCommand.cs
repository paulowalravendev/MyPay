using MyPay.Application.Abstractions.Messaging;

namespace MyPay.Application.Features.Customers.LoginCustomer;

public sealed record LoginCustomerCommand(
    string Cpf,
    string Password) : ICommand<AccessTokenResponse>;
