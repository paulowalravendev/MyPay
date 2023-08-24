namespace MyPay.Api.Controllers.Customers;

public sealed record RegisterCustomerRequest(
    string Email,
    string FullName,
    string Cpf,
    string Password);
