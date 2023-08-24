using MyPay.Domain.Abstractions;

namespace MyPay.Domain.Shopkeepers.Events;

public sealed record ShopkeeperCreatedDomainEvent(ShopkeeperId ShopkeeperId) : IDomainEvent;
