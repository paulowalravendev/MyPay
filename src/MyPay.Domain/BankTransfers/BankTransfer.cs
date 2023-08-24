using MyPay.Domain.Abstractions;
using MyPay.Domain.BankTransfers.Events;

namespace MyPay.Domain.BankTransfers;

public sealed class BankTransfer : Entity<BankTransferId>
{
    public BankTransfer(BankTransferId id, Money money, PayerId payerId, PayeeId payeeId)
        : base(id)
    {
        Money = money;
        PayerId = payerId;
        PayeeId = payeeId;
    }

    public Money Money { get; private set; }
    public PayerId PayerId { get; private set; }
    public PayeeId PayeeId { get; private set; }

    public static BankTransfer Create(Money money, PayerId payerId, PayeeId payeeId)
    {
        var bankTransfer = new BankTransfer(BankTransferId.New(), money, payerId, payeeId);
        bankTransfer.RaiseDomainEvent(new BankTransferCreatedDomainEvent(bankTransfer.Id));
        return bankTransfer;
    }
}
