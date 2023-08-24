using MyPay.Application.Abstractions.Messaging;

namespace MyPay.Application.Features.Shopkeepers.LoginShopkeeper;

public sealed record LoginShopkeeperCommand(
    string Cnpj,
    string Password) : ICommand<AccessTokenResponse>;
