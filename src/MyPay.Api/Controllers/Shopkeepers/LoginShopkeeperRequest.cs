namespace MyPay.Api.Controllers.Shopkeepers;

public sealed record LoginShopkeeperRequest(
    string Cnpj,
    string Password);