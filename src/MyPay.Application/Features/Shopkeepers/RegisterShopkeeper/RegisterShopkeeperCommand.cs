using MyPay.Application.Abstractions.Messaging;

namespace MyPay.Application.Features.Shopkeepers.RegisterShopkeeper;

public sealed record RegisterShopkeeperCommand(
    string Fullname,
    string Email,
    string Cnpj,
    string Password) : ICommand<Guid>;
