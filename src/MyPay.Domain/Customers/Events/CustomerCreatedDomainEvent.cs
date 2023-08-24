using MyPay.Domain.Abstractions;

namespace MyPay.Domain.Customers.Events;

public sealed record CustomerCreatedDomainEvent(CustomerId CustomerId) : IDomainEvent;
