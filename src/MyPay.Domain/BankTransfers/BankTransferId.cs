namespace MyPay.Domain.BankTransfers;

public record BankTransferId(Guid Value)
{
    public static BankTransferId New() => new(Guid.NewGuid());
}
