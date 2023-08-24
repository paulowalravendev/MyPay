using MyPay.Domain.Abstractions;

namespace MyPay.Domain.BankTransfers.Events;

public record BankTransferCreatedDomainEvent(BankTransferId Id) : IDomainEvent;