namespace MyPay.Api.Controllers.Shopkeepers;

public sealed record RegisterShopkeeperRequest(
    string Email,
    string FullName,
    string Cnpj,
    string Password);
