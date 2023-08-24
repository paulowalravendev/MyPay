using MyPay.Domain.Customers;
using MyPay.Domain.Shopkeepers;

namespace MyPay.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    string GenerateJwtToken(Customer customer);
    string GenerateJwtToken(Shopkeeper shopkeeper);
}
