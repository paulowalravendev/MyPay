using MyPay.Domain.Customers;

namespace MyPay.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    string GenerateJwtToken(Customer customer);
}
