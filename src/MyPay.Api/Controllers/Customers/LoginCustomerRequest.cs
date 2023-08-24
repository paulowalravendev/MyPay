namespace MyPay.Api.Controllers.Customers;

public sealed record LoginCustomerRequest(
    string Cpf,
    string Password);